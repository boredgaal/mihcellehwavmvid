using Microsoft.JSInterop;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Services
{
    public class ScrollService
    {

        private readonly IJSRuntime JSRuntime;
        private IJSObjectReference scrollScriptJsObjRef { get; set; }
        private IJSObjectReference scrollScriptMap { get; set; }

        public ScrollService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            this.JSRuntime = jsRuntime;
        }

        public async Task InitScrollService()
        {
            this.scrollScriptJsObjRef = await this.JSRuntime.InvokeAsync<IJSObjectReference>("import", "/chathubs/scrollservicejsinterop.js");
            this.scrollScriptMap = await this.scrollScriptJsObjRef.InvokeAsync<IJSObjectReference>("initscrollservice");
        }

        public void ScrollToBottom(string element)
        {
            this.scrollScriptMap.InvokeVoidAsync("scrollToBottom", element);
        }

    }
}