using Verse;
using RimWorld;
using System.Linq;

namespace ESCP_Trolls
{
    public static class Utility
    {
        public static void HealWounds(Pawn pawn, float baseNumber, float severityValue, float tendValue)
        {
			float CurrentNumber = baseNumber * pawn.health.capacities.GetLevel(PawnCapacityDefOf.BloodPumping);
			for (int i = 0; i != baseNumber; i++)
			{
				Hediff hediff;
				if (!(from hd in pawn.health.hediffSet.hediffs
					  where hd.Bleeding
					  select hd).TryRandomElement(out hediff))
				{
					return;
				}
				if (pawn.health.hediffSet.PartIsMissing(hediff.Part))
				{
					hediff.Tended(tendValue, 1);
				}
				else
				{
					hediff.Severity -= severityValue;
				}
			}
		}
    }
}
