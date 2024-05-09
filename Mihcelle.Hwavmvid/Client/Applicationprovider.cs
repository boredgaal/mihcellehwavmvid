using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Mihcelle.Hwavmvid.Shared.Models;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using Mihcelle.Hwavmvid.Alerts;

namespace Mihcelle.Hwavmvid.Client
{
    public class Applicationprovider
    {


        // app providers //
        public IHttpClientFactory ihttpclientfactory { get; set; }
        public NavigationManager navigationmanager { get; set; }
        public IJSRuntime ijsruntime { get; set; }
        public AlertsService AlertsService { get; set; }
        

        // javascript references //
        public DotNetObjectReference<Applicationprovider> dotnetobjref;
        public IJSObjectReference appjavascriptfile { get; set; }
        public IJSObjectReference appjavascriptmap { get; set; }


        // application events //
        public event Action _oncontextpagechanged;
        public void _contextpagechanged() => this._oncontextpagechanged?.Invoke();

        public event Action UpdateUI;
        public void _updateUIhasfallen() => this.UpdateUI.Invoke();

        // application _context properties //
        public AuthenticationState? _contextauth { get; set; }
        public Applicationsite _contextsite { get; set; }
        public Applicationpage? _contextpage { get; set; }
        public Applicationcontainer? _contextcontainer { get; set; }


        // signalr things //
        public HubConnection? _connection { get; set; }

        private const string unauthorizeduser = "unauthorizeduser";

        private string _applicationid = Guid.NewGuid().ToString();


        // _contextitems and draggable droppable package items //
        public List<Applicationmodulepackage>? _contextpackages { get; set; }
        public List<Applicationmodulepackage>? _contextframeworkpackages { get; set; }

        public List<Applicationcontainercolumn>? _contextcontainercolumns { get; set; }


        // constructor //
        public Applicationprovider(IHttpClientFactory ihttpclientfactory, IJSRuntime ijsruntime, NavigationManager navigationmanager, AlertsService alertsservice)
        {
            this.ihttpclientfactory = ihttpclientfactory;
            this.ijsruntime = ijsruntime;
            this.navigationmanager = navigationmanager;
            this.AlertsService = alertsservice;

            this.dotnetobjref = DotNetObjectReference.Create(this);
        }


        // initialize javascript interop and package items drag and drop //
        public async Task Initpackagemoduledraganddrop()
        {


            if (this.appjavascriptfile == null)
            {
                this.appjavascriptfile = await this.ijsruntime.InvokeAsync<IJSObjectReference>("import", "/jsinterops/applicationprovider.js");
            }

            if (this.appjavascriptfile != null)
            {
                if (this.appjavascriptfile != null && this._contextpackages != null && this._contextcontainercolumns != null)
                {

                    try
                    {
                        foreach (var packageitem in this._contextpackages)
                        {
                            var obj = await this.appjavascriptfile.InvokeAsync<IJSObjectReference>("initpackagemoduledraganddrop", this.dotnetobjref, packageitem.Id, "draggable");
                            if (obj != null)
                            {
                                await obj.InvokeVoidAsync("removeevents");
                                await obj.InvokeVoidAsync("addevents");
                            }

                            packageitem.JSObjectReference = obj;
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message); 
                    }

                    if (this.appjavascriptfile != null && this._contextframeworkpackages != null && this._contextcontainercolumns != null)
                    {
                        try
                        {
                            foreach (var packageitem in this._contextframeworkpackages)
                            {
                                var obj = await this.appjavascriptfile.InvokeAsync<IJSObjectReference>("initpackagemoduledraganddrop", this.dotnetobjref, packageitem.Id, "draggable");
                                if (obj != null)
                                {
                                    await obj.InvokeVoidAsync("removeevents");
                                    await obj.InvokeVoidAsync("addevents");
                                }

                                packageitem.JSObjectReference = obj;
                            }
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                    }

                    try
                    {
                        foreach (var columnitem in this._contextcontainercolumns)
                        {
                            var obj = await this.appjavascriptfile.InvokeAsync<IJSObjectReference>("initpackagemoduledraganddrop", this.dotnetobjref, columnitem.Id, "droppable");
                            if (obj != null)
                            {
                                await obj.InvokeVoidAsync("removeevents");
                                await obj.InvokeVoidAsync("addevents");

                                columnitem.JSObjectReference = obj;
                            }
                        }
                    }
                    catch (Exception exception) { Console.WriteLine(exception.Message); }

                }
            }
        }

        [JSInvokable("ItemDropped")]
        public async Task ItemDropped(string draggedfieldid, string droppedfieldid)
        {

            if (_contextauth?.User?.Identity?.IsAuthenticated ?? false)
            {
                try
                {

                    if (this._contextpackages != null && this._contextframeworkpackages != null)
                    {

                    var package = this._contextpackages.FirstOrDefault(item => item.Id == draggedfieldid);
                    if (package == null)
                        package = this._contextframeworkpackages.FirstOrDefault(item => item.Id == draggedfieldid);

                        if (package != null)
                        {

                            var module = new Applicationmodule()
                            {
                                Id = null,
                                Packageid = package.Id,
                                Containercolumnid = droppedfieldid,
                                Containercolumnposition = 0,
                                Assemblytype = package.Assemblytype,
                                Settingstype = package.Settingstype,
                                Createdon = DateTime.Now,
                            };

                            var client = this.ihttpclientfactory.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
                            await client.PostAsJsonAsync("api/module", module);

                            this.AlertsService.NewAlert(string.Concat("Package", " ", package.Name, "_", package.Version, " ", "dropdown succeeded."));
                            await Task.Delay(4100).ContinueWith(t => { this.navigationmanager.NavigateTo(navigationmanager.Uri, true); });
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        // signalr methods //
        public async Task<bool> Establishapplicationconnection()
        {
            try
            {

                if (_connection?.State == HubConnectionState.Disconnected
                 || _connection?.State == HubConnectionState.Connecting
                 || _connection?.State == HubConnectionState.Reconnecting)
                {
                    Console.WriteLine("User not connected..");
                }

                _connection = Buildapplicationhubconnection();
                await _connection.StartAsync().ContinueWith(async task =>
                {
                    if (task.Status == TaskStatus.RanToCompletion || task.Status == TaskStatus.Faulted)
                    {

                        await _connection.SendAsync("Establishapplicationconnection").ContinueWith((task) =>
                        {
                            if (task.Status == TaskStatus.RanToCompletion || task.Status == TaskStatus.Faulted)
                            {

                            }
                        });
                    }
                });

                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return false;
        }
        private HubConnection Buildapplicationhubconnection()
        {

            StringBuilder urlBuilder = new StringBuilder();
            var hubconnectionuri = navigationmanager.BaseUri + "api/applicationhub";

            urlBuilder.Append(hubconnectionuri);
            urlBuilder.Append("?username=" + unauthorizeduser);

            var url = urlBuilder.ToString();
            return new HubConnectionBuilder().WithUrl(url, options =>
            {
                //options.Cookies.Add(this.IdentityCookie);
                options.Headers.Add("applicationid", _applicationid);
                options.Headers.Add("platform", "mihcelle.hwavmvid");
                options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
            })
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.WriteIndented = false;
                options.PayloadSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
                options.PayloadSerializerOptions.AllowTrailingCommas = true;
                options.PayloadSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
                options.PayloadSerializerOptions.DefaultBufferSize = 4096;
                options.PayloadSerializerOptions.MaxDepth = 41;
                options.PayloadSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            })
            .Build();
        }

    }
}
