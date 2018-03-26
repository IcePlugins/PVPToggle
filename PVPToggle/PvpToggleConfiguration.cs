using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtraConcentratedJuice.PVPToggle
{
    public class PvpToggleConfiguration : IRocketPluginConfiguration
    {
        public bool pveByDefault;
        public bool ignoreAdmins;
        public bool enableWarningEffect;
        public ushort warningEffectId;
        public int warningCooldown;
        public float warningScreentime;
        public bool enableIndicatorEffect;
        public bool indicatorByDefault;
        public ushort indicatorEffectId;

        public void LoadDefaults()
        {
            pveByDefault = false;
            ignoreAdmins = false;
            enableWarningEffect = true;
            warningEffectId = 60125;
            warningCooldown = 3;
            warningScreentime = 2.0F;
            enableIndicatorEffect = true;
            indicatorByDefault = true;
            indicatorEffectId = 60126;
        }
    }
}
