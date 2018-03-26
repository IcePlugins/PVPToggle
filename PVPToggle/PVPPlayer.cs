using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtraConcentratedJuice.PVPToggle
{
    public class PvpPlayer : UnturnedPlayerComponent
    {
        public bool PvpEnabled { get; set; }
        public bool IndicatorEnabled { get; set; }
        public DateTime LastWarned { get; set; }

        public PvpPlayer()
        {
            PvpEnabled = !Util.Config().pveByDefault;
            IndicatorEnabled = Util.Config().indicatorByDefault;
            LastWarned = DateTime.Now;
        }
    }
}
