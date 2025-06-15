using Verse;
using RimWorld;

namespace ESCP_Trolls
{
    public class GenStep_AfflictedTroll : GenStep
	{
		public override int SeedPart
		{
			get
			{
				return 918672345;
			}
		}
		public override void Generate(Map map, GenStepParams parms)
		{
			TraverseParms traverseParams = TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false, false, false).WithFenceblocked(true);
			IntVec3 root;
			if (RCellFinder.TryFindRandomCellNearTheCenterOfTheMapWith((IntVec3 x) => x.Standable(map) && !x.Fogged(map) && map.reachability.CanReachMapEdge(x, traverseParams) && x.GetRoom(map).CellCount >= this.MinRoomCells, map, out root))
			{
				IntVec3 loc = CellFinder.RandomSpawnCellForPawnNear(root, map, 10);
				Pawn pawn = PawnGenerator.GeneratePawn(parms.sitePart.parms.animalKind, null);
				GenSpawn.Spawn(pawn, loc, map, Rot4.Random, WipeMode.Vanish, false);

				pawn.health.hediffSet.hediffs.Clear();

				pawn.health.AddHediff(HediffDefOf.ESCP_AfflictedTroll).TryGetComp<HediffComp_AfflictedTroll>().site = parms.sitePart.site;

				int num = Rand.RangeInclusive(3, 7);
				for (int i = 0; i < num; i++)
				{
					pawn.health.AddHediff(HediffDefOf.ESCP_AfflictedTrollBattleScar, pawn.health.hediffSet.GetRandomNotMissingPart(Rand.Chance(0.5f) ? DamageDefOf.Stab : DamageDefOf.Cut));
				}
				pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent);
			}
		}

		private readonly int MinRoomCells = 225;
	}
}
