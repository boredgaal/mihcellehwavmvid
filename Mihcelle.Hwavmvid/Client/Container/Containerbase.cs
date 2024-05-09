using Microsoft.AspNetCore.Components;
using Mihcelle.Hwavmvid.Shared.Constants;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Net.Http.Json;

namespace Mihcelle.Hwavmvid.Client.Container
{
    public class Containerbase : ComponentBase, IDisposable
    {


        [Inject] public required IHttpClientFactory ihttpclientfactory { get; set; }
        [Inject] public required Applicationprovider applicationprovider { get; set; }
        [Inject] public required NavigationManager navigationmanager { get; set; }

        public required HttpClient httpclient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.applicationprovider._oncontextpagechanged += async () => await this.Contextpagechanged();
            await base.OnInitializedAsync();
        }

        protected async Task Contextpagechanged()
        {

            try
            {

                this.httpclient = this.ihttpclientfactory.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");

                this.applicationprovider._contextcontainer = null;
                this.applicationprovider._contextcontainercolumns = null;

                await InvokeAsync(async () =>
                {
                    this.applicationprovider._contextcontainer = await this.httpclient.GetFromJsonAsync<Applicationcontainer?>(string.Concat("api/container/", this.applicationprovider?._contextpage.Id));
                    this.applicationprovider._updateUIhasfallen();
                    this.StateHasChanged();
                });

                await InvokeAsync(async () =>
                {
                    this.applicationprovider._contextcontainercolumns = await this.httpclient.GetFromJsonAsync<List<Applicationcontainercolumn>>(string.Concat("api/containercolumns/", this.applicationprovider?._contextcontainer.Id));
                    this.applicationprovider._updateUIhasfallen();
                    this.StateHasChanged();
                });

                await Task.Delay(4100).ContinueWith(async (task) =>
                {
                    await InvokeAsync(async () =>
                    {
                        await this.applicationprovider.Initpackagemoduledraganddrop();
                        this.applicationprovider._updateUIhasfallen();
                        this.StateHasChanged();
                    });
                });

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        protected async Task Setcontainertype()
        {

            string targetcontainertype = string.Empty;

            if (this.applicationprovider._contextcontainer?.Containertype == Applicationcontainertype.Boxed.ToString())
                targetcontainertype = "container-fluid";

            if (this.applicationprovider._contextcontainer?.Containertype == Applicationcontainertype.Fullwidth.ToString())
                targetcontainertype = "container";

            await this.httpclient.GetAsync(string.Concat("api/container/", this.applicationprovider._contextcontainer?.Id, "/", targetcontainertype));
            this.navigationmanager.NavigateTo(this.navigationmanager.Uri, true);
        }
        public void Dispose()
        {
            this.applicationprovider._oncontextpagechanged -= async () => await this.Contextpagechanged();
        }

    }
}
