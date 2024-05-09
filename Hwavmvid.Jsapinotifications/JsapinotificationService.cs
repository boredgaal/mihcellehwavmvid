using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Jsapinotifications
{

    public class JsapinotificationService : IDisposable
    {

        private IJSRuntime jsRuntime { get; set; }
        private IJSObjectReference javascriptFile { get; set; }

        public JsapinotificationService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task InitJsapinotifications()
        {
            this.javascriptFile = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "/_content/Mihcelle.Hwavmvid.Jsapinotifications/jsapinotificationjsinterop.js");
        }

        public async Task<bool> RequestPermission()
        {
            return await this.javascriptFile.InvokeAsync<bool>("requestpermission");
        }

        public async Task ShowNotification(Jsapinotification item)
        {
            await this.javascriptFile.InvokeVoidAsync("shownotification", 
                item.Id, item.Title, item.Dir, item.Lang, item.Body, item.Tag, item.Icon, item.Data);
        }

        public void Dispose()
        {
            if (javascriptFile != null)
                this.javascriptFile.DisposeAsync();
        }

    }
}