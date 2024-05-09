using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Cookies
{
    public class Cookiesprovider
    {

        public IJSRuntime Jsruntime { get; set; }
        public IJSObjectReference Module { get; set; }
        public IJSObjectReference Cookiemap { get; set; }

        public DotNetObjectReference<Cookiesprovider> DotNetObjectRef;

        public Cookiesprovider(IJSRuntime jsRuntime)
        {
            this.Jsruntime = jsRuntime;
            this.DotNetObjectRef = DotNetObjectReference.Create(this);
        }
        public async Task Initcookiesprovider()
        {
            if (this.Module == null || this.Cookiemap == null)
            {
                this.Module = await this.Jsruntime.InvokeAsync<IJSObjectReference>("import", "/_content/Mihcelle.Hwavmvid.Cookies/cookiesprovider.js");
                this.Cookiemap = await this.Module.InvokeAsync<IJSObjectReference>("initcookiesprovider");
            }
        }

        public async Task<string> Getcookie(string cookiename)
        {
            var cookievalue = await this.Cookiemap.InvokeAsync<string>("getCookie", cookiename); return cookievalue;
        }
        public async Task Setcookie(string cookiename, string cookievalue, int expirationdays)
        {
            await this.Cookiemap.InvokeVoidAsync("setCookie", cookiename, cookievalue, expirationdays);
        }

    }
}
