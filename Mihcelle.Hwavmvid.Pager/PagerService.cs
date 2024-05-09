using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Pager
{
    public class Pagerservice<TPagerItem>
    {

        private IJSRuntime JSRuntime { get; set; }
        private IJSObjectReference pagerJsObjectReference { get; set; }
        private IJSObjectReference pagerMap { get; set; }

        public event Action<List<TPagerItem>, string> OnRetrievedItems;
        public event Action<Pagerevent<TPagerItem>> OnRemoveItem;
        public event Action<string> OnUpdateContext;
        public event Action<Exception, string> OnError;

        public Pagerservice(IJSRuntime jsRuntime)
        {
            this.JSRuntime = jsRuntime;
        }

        public async Task InitPagerService()
        {
            this.pagerJsObjectReference = await this.JSRuntime.InvokeAsync<IJSObjectReference>("import", "/_content/Mihcelle.Hwavmvid.Pager/blazorpagerjsinterop.js");
            this.pagerMap = await this.pagerJsObjectReference.InvokeAsync<IJSObjectReference>("initpager");
        }
        public void ExposeItems(List<TPagerItem> items, string apiqueryid)
        {
            this.OnRetrievedItems?.Invoke(items, apiqueryid);
        }
        public async Task ScrollTop(string elementId)
        {
            if(this.pagerMap != null)
            {
                await this.pagerMap.InvokeVoidAsync("scrollToElement", elementId);
            }
        }
        public void RemoveItem(TPagerItem item, string apiQueryId)
        {
            this.OnRemoveItem?.Invoke(new Pagerevent<TPagerItem>() { Item = item, ApiQueryId = apiQueryId });
        }
        public void UpdateContext(string apiQueryId)
        {
            this.OnUpdateContext?.Invoke(apiQueryId);
        }
        public void ThrowError(Exception exception, string apiQueryId)
        {
            this.OnError?.Invoke(exception, apiQueryId);
        }

    }
}
