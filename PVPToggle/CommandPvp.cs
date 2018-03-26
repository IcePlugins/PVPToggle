using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Steamworks;
using UnityEngine;
using SDG.Unturned;

namespace ExtraConcentratedJuice.PVPToggle
{
    public class CommandPvp : IRocketCommand
    {
        #region Properties

        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "pvp";

        public string Help => "Toggles PVP mode on or off or allows you to check the status of others.";

        public string Syntax => "/pvp <player (optional)>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "pvptoggle.pvp" };

        #endregion

        public void Execute(IRocketPlayer caller, string[] args)
        {
            if (args.Length == 1)
            {
                UnturnedPlayer other = UnturnedPlayer.FromName(args[0]);

                if (other == null)
                    UnturnedChat.Say(caller, Util.Translate("invalid_player"), Color.red);
                else
                    UnturnedChat.Say(caller, Util.Translate("other", other.DisplayName,
                        other.GetComponent<PvpPlayer>().PvpEnabled ? "on" : "off"));
            }
            else
            {
                PvpPlayer p = ((UnturnedPlayer)caller).GetComponent<PvpPlayer>();
                p.PvpEnabled = !p.PvpEnabled;
                UnturnedChat.Say(caller, Util.Translate("pvp_toggled", p.PvpEnabled ? "on" : "off"));

                if (p.IndicatorEnabled)
                {
                    CSteamID id = ((UnturnedPlayer)caller).CSteamID;
                    EffectManager.sendUIEffect(Util.Config().indicatorEffectId, 1201, id, true, p.PvpEnabled ? "PVP" : "PVE");
                }
            }
        }
    }
}