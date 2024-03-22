using Verse;
using RimWorld;

namespace ESCP_Trolls
{
	[DefOf]
	public static class HediffDefOf
	{

		static HediffDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(HediffDefOf));
		}

		public static HediffDef ESCP_AfflictedTroll;
		public static HediffDef ESCP_AfflictedTrollBattleScar;
	}
}
