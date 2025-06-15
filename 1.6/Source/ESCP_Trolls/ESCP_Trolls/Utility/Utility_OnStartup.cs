using Verse;
using System.Collections.Generic;

namespace ESCP_Trolls
{

	[StaticConstructorOnStartup]
	public static class Utility_OnStartup
	{
		public static List<PawnKindDef> trollKindDefs = new List<PawnKindDef> { };
		public static string tileFeatureRequirement_Tooltip;

		static Utility_OnStartup()
        {
			FetchTrollKindDefs();
			GenerateTooltip_TileFeatureRequirement();
        }

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

		public static void GenerateTooltip_TileFeatureRequirement()
		{
			string output = "";
			foreach(PawnKindDef kindDef in trollKindDefs)
            {
				BiomeFeatureRequirements props = BiomeFeatureRequirements.Get(kindDef.race);
                if (props != null)
                {
					output += "\n- " + kindDef.race.label.CapitalizeFirst();
                }
            }

			tileFeatureRequirement_Tooltip = output;
		}
	}
}
