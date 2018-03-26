using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Steamworks;
using UnityEngine;
using SDG.Unturned;

namespace ExtraConcentratedJuice.PVPToggle
{
    public class CommandIndicator : IRocketCommand
    {
        #region Properties

        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "indicator";

        public string Help => "Toggles the PVP indicator on or off.";

        public string Syntax => "/indicator";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "pvptoggle.indicator" };

        #endregion

        public void Execute(IRocketPlayer caller, string[] args)
        {
            if (!Util.Config().enableIndicatorEffect)
            {
                UnturnedChat.Say(caller, "not_enabled", Color.red);
                return;
            }

            PvpPlayer p = ((UnturnedPlayer)caller).GetComponent<PvpPlayer>();

            if (p.IndicatorEnabled)
                EffectManager.askEffectClearByID(Util.Config().indicatorEffectId, ((UnturnedPlayer)caller).CSteamID);
            else
                EffectManager.sendUIEffect(Util.Config().indicatorEffectId, 1201, ((UnturnedPlayer)caller).CSteamID, true, p.PvpEnabled ? "PVP" : "PVE");

            p.IndicatorEnabled = !p.IndicatorEnabled;
            UnturnedChat.Say(caller, Util.Translate("indicator_toggled", p.IndicatorEnabled ? "on" : "off"));
        }
    }
}