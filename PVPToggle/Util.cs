using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtraConcentratedJuice.PVPToggle
{
    public static class Util
    {
        public static PvpToggleConfiguration Config() => PvpToggle.instance.Configuration.Instance;

        public static string Translate(string TranslationKey, params object[] Placeholders) =>
            PvpToggle.instance.Translations.Instance.Translate(TranslationKey, Placeholders);
    }
}
