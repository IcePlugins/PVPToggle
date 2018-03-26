using Rocket.Core.Plugins;
using System;
using System.Linq;
using System.Text;
using Rocket.API.Collections;
using SDG.Unturned;
using Steamworks;
using UnityEngine;
using Rocket.Unturned.Player;
using Rocket.API;
using Rocket.Core.Commands;
using Rocket.Unturned.Chat;
using System.Collections;
using Rocket.Unturned.Events;
using Rocket.Unturned;
using System.Threading;

namespace ExtraConcentratedJuice.PVPToggle
{
    public class PvpToggle : RocketPlugin<PvpToggleConfiguration>
    {
        public static PvpToggle instance;

        protected override void Load()
        {
            instance = this;
            DamageTool.playerDamaged += OnDamage;
            U.Events.OnPlayerConnected += OnPlayerConnected;
        }

        protected override void Unload()
        {
            DamageTool.playerDamaged -= OnDamage;
            U.Events.OnPlayerConnected -= OnPlayerConnected;
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            PvpPlayer p = player.GetComponent<PvpPlayer>();

            if (p.IndicatorEnabled)
                EffectManager.sendUIEffect(Util.Config().indicatorEffectId, 1201, player.CSteamID, true, p.PvpEnabled ? "PVP" : "PVE");
        }

        private void OnDamage(Player player, ref EDeathCause cause, ref ELimb limb, ref CSteamID killer, ref Vector3 direction, ref float damage, ref float times, ref bool canDamage)
        {
            if (player == null || killer == null)
                return;

            UnturnedPlayer p = UnturnedPlayer.FromPlayer(player);
            UnturnedPlayer k = UnturnedPlayer.FromCSteamID(killer);

            if (p.CSteamID == killer)
                return;

            if ((p.IsAdmin || k.IsAdmin) && Util.Config().ignoreAdmins)
                return;

            if (p.GetPermissions().Any(x => x.Name == "pvptoggle.ignore") || k.GetPermissions().Any(x => x.Name == "pvptoggle.ignore"))
                return;

            if (!p.GetComponent<PvpPlayer>().PvpEnabled || !k.GetComponent<PvpPlayer>().PvpEnabled)
            {
                if (Util.Config().enableWarningEffect && (DateTime.Now - k.GetComponent<PvpPlayer>().LastWarned).TotalSeconds > Util.Config().warningCooldown)
                {
                    EffectManager.sendUIEffect(Util.Config().warningEffectId, 1200, killer, true);
                    CSteamID attacker = killer;
                    StartCoroutine(DelayedInvoke(Util.Config().warningScreentime,
                        () => EffectManager.askEffectClearByID(Util.Config().warningEffectId, attacker)));
                    k.GetComponent<PvpPlayer>().LastWarned = DateTime.Now;
                }
                damage = 0;
                canDamage = false;
            }
        }

        private IEnumerator DelayedInvoke(float time, System.Action action)
        {
            yield return new WaitForSeconds(time);
            action();
        }

        public override TranslationList DefaultTranslations =>
            new TranslationList
            {
                { "pvp_toggled", "You have toggled PVP {0}." },
                { "indicator_toggled", "Indicator turned {0}." },
                { "invalid_player", "That player does not seem to exist." },
                { "other", "{0} has their PVP turned {1}." },
                { "not_enabled", "This feature is not enabled on this server." }
            };
    }
}
