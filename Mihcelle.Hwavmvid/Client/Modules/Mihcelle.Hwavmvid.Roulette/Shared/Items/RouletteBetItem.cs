using Mihcelle.Hwavmvid.Modules.Roulette.Shared.Enums;

namespace Mihcelle.Hwavmvid.Modules.Roulette.Shared.Items
{
    public class RouletteBetItem
    {

        public string Id { get; set; }
        public RouletteCoin Coin { get; set; }
        public RoulettesurfaceNumber Surfacenumber { get; set; }
        public RouletteBetoptionsItem Betoption { get; set; }
        public RouletteBetStatus Status { get; set; }

    }
}
