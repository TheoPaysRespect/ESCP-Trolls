using Verse;
using RimWorld;

namespace ESCP_Trolls
{
    public class IngestionOutcomeDoer_DelayedRegeneration : IngestionOutcomeDoer
	{
		protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested)
		{
			float q;
			ingested.TryGetQuality(out QualityCategory qc);
			q = (float)qc + 1;
			q /= 24;
			Hediff hediff = HediffMaker.MakeHediff(Hediff, pawn, null);
			hediff.Severity = q;
			pawn.health.AddHediff(hediff, null, null, null);

		}

		public HediffDef Hediff;
	}
}
