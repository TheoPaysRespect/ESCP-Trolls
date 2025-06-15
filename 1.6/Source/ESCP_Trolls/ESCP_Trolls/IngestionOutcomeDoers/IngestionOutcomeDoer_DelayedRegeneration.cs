using Verse;
using RimWorld;

namespace ESCP_Trolls
{
    public class IngestionOutcomeDoer_DelayedRegeneration : IngestionOutcomeDoer
	{
		protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
		{
			float q;
			ingested.TryGetQuality(out QualityCategory qc);
			q = (float)qc + 1;
			q /= 24;
			Hediff hediff = HediffMaker.MakeHediff(hediffDef, pawn, null);
			hediff.Severity = q;
			pawn.health.AddHediff(hediff, null, null, null);

		}

        public HediffDef hediffDef;
    }
}
