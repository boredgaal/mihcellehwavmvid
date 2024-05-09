using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;
using Mihcelle.Hwavmvid.Cookies;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Net.Http.Json;

namespace Mihcelle.Hwavmvid.Client
{
    public class Mainlayoutbase : LayoutComponentBase, IDisposable
    {


        [Inject] public required Applicationprovider applicationprovider { get; set; }
        [Inject] public NavigationManager? navigationmanager { get; set; }
        [Inject] public AuthenticationStateProvider? authenticationstateprovider { get; set; }
        [Inject] public IConfiguration? Configuration { get; set; }
        [Inject] public IHttpClientFactory? ihttpclientfactory { get; set; }
        [Inject] public Cookiesprovider? Cookiesprovider { get; set; }


        public bool? framework_installed { get; set; } = null;
        public List<Applicationpage>? _contextpages { get; set; } = new List<Applicationpage>();


        protected override async Task OnInitializedAsync()
        {

            this.framework_installed = !string.IsNullOrEmpty(Configuration?["installation:createdon"]);
            if (this.framework_installed == true)
            {

                this.applicationprovider.UpdateUI += async () => await this.Updatemainlayout();
                this.applicationprovider._contextauth = await this.authenticationstateprovider.GetAuthenticationStateAsync();

                var client = this.ihttpclientfactory?.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
                this.applicationprovider._contextsite = await client.GetFromJsonAsync<Applicationsite>("api/site");
                this._contextpages = await client.GetFromJsonAsync<List<Applicationpage>>("api/page/bysideid/" + this.applicationprovider?._contextsite?.Id);
            }

            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {

                // implemented in program.cs
                /*if (framework_installed == false)
                {

                    // default auth cookie name
                    // .AspNetCore.Identity.Application

                    await this.Cookiesprovider.Initcookiesprovider();
                    var auth_cookie_name = Mihcelle.Hwavmvid.Shared.Constants.Authentication.Authcookiename;
                    var cookievalue = await this.Cookiesprovider.Getcookie(auth_cookie_name);
                    await this.Cookiesprovider.Setcookie(auth_cookie_name, cookievalue, -1);
                }
                */

                await this.applicationprovider.Establishapplicationconnection();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public JsonSerializerOptions jsonserializeroptions { get; set; } = new JsonSerializerOptions()
        {
            WriteIndented = false,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
            AllowTrailingCommas = true,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals,
            DefaultBufferSize = 4096,
            MaxDepth = 41,
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
            PropertyNamingPolicy = null,
        };

        protected async Task Updatemainlayout()
        {
            await InvokeAsync(() =>
            {
                this.StateHasChanged();
            });
        }

        public void Dispose()
        {
            this.applicationprovider.UpdateUI -= async () => await this.Updatemainlayout();
        }
    }
}
