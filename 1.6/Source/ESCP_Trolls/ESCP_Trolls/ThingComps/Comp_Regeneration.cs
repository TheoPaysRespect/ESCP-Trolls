using Verse;
using RimWorld;

namespace ESCP_Trolls
{
    public class Comp_Regeneration : ThingComp
	{

		public int ticks = 0;

		public CompProperties_Regeneration Props
		{
			get
			{
				return (CompProperties_Regeneration)this.props;
			}
		}

		public override void CompTick()
		{
			base.CompTick();
			Pawn pawn = parent as Pawn;
			if (ticks >= Props.Ticks)
			{
				if (pawn.IsBurning() || pawn.Dead)
				{
					ticks = 0;
					return;
				}
				else
				{
					Utility.HealWounds(pawn, Props.BaseNumber, Props.Severity, Props.Tend);
				}
				ticks = 0;
			}
			else ticks++;
		}
	}
}
