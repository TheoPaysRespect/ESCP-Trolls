using Verse;
using RimWorld;
using RimWorld.Planet;

namespace ESCP_Trolls
{
    public class HediffComp_AfflictedTroll : HediffComp
	{

		public HediffCompProperties_AfflictedTroll Props
		{
			get
			{
				return (HediffCompProperties_AfflictedTroll)this.props;
			}
		}

		public Site site;

		public override void CompExposeData()
		{
			base.CompExposeData();
			Scribe_References.Look(ref site, "afflictedTroll_site");
		}

        public override void Notify_PawnKilled()
        {
            base.Notify_PawnKilled();
            if (Props.questEnd && site != null)
            {
                QuestUtility.SendQuestTargetSignals(site.questTags, "AfflictedTrollSlain", this.Named("SUBJECT"));
            }
        }
    }
}
