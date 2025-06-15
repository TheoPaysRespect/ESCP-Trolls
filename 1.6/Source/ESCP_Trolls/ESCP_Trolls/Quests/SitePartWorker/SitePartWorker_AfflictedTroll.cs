using System.Collections.Generic;
using RimWorld.Planet;
using RimWorld.QuestGen;
using Verse;
using Verse.Grammar;
using RimWorld;

namespace ESCP_Trolls
{
    public class SitePartWorker_AfflictedTroll : SitePartWorker
    {
        public override bool IsAvailable()
        {
            return base.IsAvailable() && Trolls_ModSettings.EnableTrollHuntQuest;
        }

        public override void Init(Site site, SitePart sitePart)
        {
            base.Init(site, sitePart);

            sitePart.parms.animalKind = Utility_OnStartup.trollKindDefs.RandomElement();
        }

        public override void Notify_GeneratedByQuestGen(SitePart part, Slate slate, List<Rule> outExtraDescriptionRules, Dictionary<string, string> outExtraDescriptionConstants)
        {
            base.Notify_GeneratedByQuestGen(part, slate, outExtraDescriptionRules, outExtraDescriptionConstants);
            outExtraDescriptionRules.Add(new Rule_String("animalKind_label", part.parms.animalKind.label));
        }

        public override string GetPostProcessedThreatLabel(Site site, SitePart sitePart)
        {
            return base.GetPostProcessedThreatLabel(site, sitePart) + ": " + sitePart.parms.animalKind.label;
        }
    }
}
