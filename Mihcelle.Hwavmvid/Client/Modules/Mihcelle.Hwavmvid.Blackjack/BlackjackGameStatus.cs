using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Modules.Blackjack
{
    public enum BlackjackGameStatus
    {

        NewGame,
        InitCardDeal,
        ClientGameplay,
        DealerGameplay,
        ClientWon,
        DealerWon,

    }
}
