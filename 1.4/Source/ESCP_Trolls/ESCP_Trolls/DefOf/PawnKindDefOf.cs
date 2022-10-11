using Verse;
using RimWorld;

namespace ESCP_Trolls
{
	[DefOf]
	public static class PawnKindDefOf
	{

		static PawnKindDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
		}

		public static PawnKindDef ESCP_TrollAnima;

	}
}
