using System.Linq;
using Verse;
using RimWorld;

namespace ESCP_Trolls
{
    public class IngestionOutcomeDoer_Regeneration : IngestionOutcomeDoer
	{
		protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
		{
			float q;
			ingested.TryGetQuality(out QualityCategory qc);
			q = (float)qc + 1;
			for (int i = 0; i != q; i++)
			{
				Hediff hediff;
				if (!(from hd in pawn.health.hediffSet.hediffs
					  where hd.Bleeding
					  select hd).TryRandomElement(out hediff))
				{
					return;
				}
				float tq = (q / 100) * 20;
				hediff.Tended(tq, 1);
			}
		}
	}
}
