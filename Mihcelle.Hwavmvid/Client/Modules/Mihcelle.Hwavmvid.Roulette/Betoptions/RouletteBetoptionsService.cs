using Mihcelle.Hwavmvid.Modules.Roulette.Shared.Events;
using Mihcelle.Hwavmvid.Modules.Roulette.Shared.Items;
using Mihcelle.Hwavmvid.Modules.Roulette.Shared.Enums;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Modules.Roulette.Betoptions
{

    public class RouletteBetoptionsService : IDisposable
    {

        private IJSObjectReference javascriptfile;
        private IJSRuntime jsruntime;

        public List<RouletteBetoptionsItem> Items { get; set; } = new List<RouletteBetoptionsItem>();

        public RouletteBetoptionsService(IJSRuntime jsruntime)
        {
            this.jsruntime = jsruntime;
            foreach (var betoptionstype in Enum.GetValues<RouletteBetoptionsType>())
            {
                this.Items.Add(new RouletteBetoptionsItem(betoptionstype));
            }
        }
        public async Task InitRouletteService()
        {
            this.javascriptfile = await this.jsruntime.InvokeAsync<IJSObjectReference>("import", "/roulette/roulettebetoptionsjsinterop.js");
        }
        public async Task<string> Prompt(string message)
        {
            return await this.javascriptfile.InvokeAsync<string>("showPrompt", message);
        }
        public void Dispose()
        {
            if (javascriptfile != null)
                this.javascriptfile.DisposeAsync();
        }

    }
}