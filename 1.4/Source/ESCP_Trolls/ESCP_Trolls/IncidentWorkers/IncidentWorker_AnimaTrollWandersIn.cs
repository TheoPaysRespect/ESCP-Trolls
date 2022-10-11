using Verse;
using RimWorld;

namespace ESCP_Trolls
{
    public class IncidentWorker_AnimaTrollWandersIn : IncidentWorker
	{
		protected override bool CanFireNowSub(IncidentParms parms)
		{
			Map map = (Map)parms.target;
            return this.TryFindEntryCell(map, out _);
        }
		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			IntVec3 intVec;
			if (!this.TryFindEntryCell(map, out intVec))
			{
				return false;
			}
			PawnKindDef troll = PawnKindDefOf.ESCP_TrollAnima;
            if (!RCellFinder.TryFindRandomCellOutsideColonyNearTheCenterOfTheMap(intVec, map, 10f, out IntVec3 invalid))
            {
                invalid = IntVec3.Invalid;
            }
            Pawn pawn = null;
			IntVec3 loc = CellFinder.RandomClosewalkCellNear(intVec, map, 10, null);
			pawn = PawnGenerator.GeneratePawn(troll, null);
			GenSpawn.Spawn(pawn, loc, map, Rot4.Random, WipeMode.Vanish, false);
			if (invalid.IsValid)
			{
				pawn.mindState.forcedGotoPosition = CellFinder.RandomClosewalkCellNear(invalid, map, 10, null);
			}

			Find.LetterStack.ReceiveLetter("ESCP_Trolls_LetterLabelTrollWandersIn".Translate(troll.label).CapitalizeFirst(), "ESCP_Trolls_LetterTrollWandersIn".Translate(troll.label), LetterDefOf.PositiveEvent, pawn, null, null);
			return true;
		}
		//

		private bool TryFindEntryCell(Map map, out IntVec3 cell)
		{
			return RCellFinder.TryFindRandomPawnEntryCell(out cell, map, CellFinder.EdgeRoadChance_Animal + 0.2f, false, null);
		}
	}
}
