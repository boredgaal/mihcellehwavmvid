using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Mihcelle.Hwavmvid.Client
{
    public class Applicationmodulesettingsservice
    {

        public IHttpClientFactory ihttpclientfactory { get; set; }

        public Applicationmodulesettingsservice(IHttpClientFactory ihttpclientfactory)
        {
            this.ihttpclientfactory = ihttpclientfactory;
        }

        public async Task<Dictionary<string, string>?> GetModuleSettingsAsync(string moduleid)
        {
            try
            {
                var client = this.ihttpclientfactory.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
                var dictitem = await client.GetFromJsonAsync<Dictionary<string, string>>("api/modulesettings/" + moduleid);
                return dictitem;
            }
            catch (Exception exception) {
                return null; }
        }

        public string GetSetting(Dictionary<string, string>? modulesettings, string key, string defaultvalue)
        {
            if (modulesettings == null || !modulesettings.ContainsKey(key))
                return defaultvalue;

            return modulesettings[key];
        }

        public async Task SetSetting(string moduleid, string key, string value)
        {
            var client = this.ihttpclientfactory.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
            await client.GetAsync("api/modulesettings/" + moduleid + "/" + key + "/" + value);
        }

    }
}
