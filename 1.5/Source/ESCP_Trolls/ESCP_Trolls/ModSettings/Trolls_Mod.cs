using UnityEngine;
using Verse;
using System;

namespace ESCP_Trolls
{
    public class Trolls_Mod : Mod
    {
        Trolls_ModSettings settings;

        public Trolls_Mod(ModContentPack contentPack) : base(contentPack)
        {
            this.settings = GetSettings<Trolls_ModSettings>();
        }

        public override string SettingsCategory() => "ESCP_Trolls_ModName".Translate();

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(inRect);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("ESCP_Trolls_EnableTrollHuntQuest".Translate(), ref settings.enableTrollHuntQuest);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("ESCP_Trolls_EnableAnimaTrollIncident".Translate(), ref settings.enableAnimaTrollIncident);
            listing_Standard.Gap();

            listing_Standard.CheckboxLabeled("ESCP_Trolls_TileFeatureRequirement".Translate(), ref settings.tileFeatureRequirement, "ESCP_Trolls_TileFeatureRequirement_Tooltip".Translate() + Utility_OnStartup.tileFeatureRequirement_Tooltip);
            listing_Standard.Gap();

            listing_Standard.Label("ESCP_Trolls_TrollCommonalityMult".Translate() + " (" + settings.trollCommonalityMult + "x)");
            settings.trollCommonalityMult = (float)Math.Round(listing_Standard.Slider(settings.trollCommonalityMult, 0.1f, 10f) * 10) / 10;
            listing_Standard.Gap();

            //reset
            Rect rectDefault = listing_Standard.GetRect(30f);
            TooltipHandler.TipRegion(rectDefault, "ESCP_Reset".Translate());
            if (Widgets.ButtonText(rectDefault, "ESCP_Reset".Translate(), true, true, true))
            {
                settings.ResetSettings();
            }

            listing_Standard.End();
            base.DoSettingsWindowContents(inRect);
        }
    }
}
