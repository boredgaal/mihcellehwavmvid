using Microsoft.AspNetCore.Components;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Net.Http.Json;
using Mihcelle.Hwavmvid.Client.Container;

namespace Mihcelle.Hwavmvid.Client.Pages
{
    public class Indexbase : ComponentBase, IDisposable
    {

        [Inject] public IHttpClientFactory ihttpclientfactory { get; set; }
        [Inject] public NavigationManager navigationmanager { get; set; }
        [Inject] public Applicationprovider applicationprovider { get; set; }


        [Parameter] 
        public string _contextpageurlpath { get; set; }
        private string _contextpagealreadyrequested { get; set; }

        private const string frontpagename = "mihcellehwavmvid_corporatcc";

        protected override async Task OnInitializedAsync()
        {
            this.navigationmanager.LocationChanged += async (obj, ev) => await this.Getcontextpage(this._contextpageurlpath);
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {

            if (string.IsNullOrEmpty(this._contextpageurlpath))
                this._contextpageurlpath = frontpagename;

            await this.Getcontextpage(this._contextpageurlpath);
            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Task.Delay(1400).ContinueWith((task) =>
                {
                    this.applicationprovider._contextpagechanged();
                    this.applicationprovider._updateUIhasfallen();
                });
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task Getcontextpage(string path)
        {
            try
            {
                if (path != this._contextpagealreadyrequested)
                {
                    this._contextpagealreadyrequested = path;
                    var client = this.ihttpclientfactory?.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
                    this.applicationprovider._contextpage = await client.GetFromJsonAsync<Applicationpage>(string.Concat("api/page/", path));
                    this.applicationprovider._contextpagechanged();
                }
            }
            catch (Exception exception) { Console.WriteLine(exception.Message); }
        }

        public void Dispose()
        {
            this.navigationmanager.LocationChanged -= async (obj, ev) => await this.Getcontextpage(this._contextpageurlpath);
        }

    }
}
