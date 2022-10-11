using Verse;
using RimWorld;
using System.Linq;
using System.Collections.Generic;

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

	[StaticConstructorOnStartup]
	public static class Utility_OnStartup
	{
		public static List<PawnKindDef> trollKindDefs = new List<PawnKindDef> { };
		public static void FetchTrollKindDefs()
		{
			List<PawnKindDef> allDefsListForReading = DefDatabase<PawnKindDef>.AllDefsListForReading;
			for (int i = 0; i < allDefsListForReading.Count; i++)
			{
				PawnKindDef pawnKindDef = allDefsListForReading[i];
				bool flag = pawnKindDef.race.race.Animal && pawnKindDef.race.tradeTags != null && pawnKindDef.race.tradeTags.Contains("ESCP_Troll");
				if (flag)
				{
					trollKindDefs.Add(pawnKindDef);
				}
			}
		}
	}
}
