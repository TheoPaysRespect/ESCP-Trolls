using Verse;
using RimWorld;

namespace ESCP_Trolls
{
    public class HediffComp_Regeneration : HediffComp
	{

		public int ticks = 0;

		public HediffCompProperties_Regeneration Props
		{
			get
			{
				return (HediffCompProperties_Regeneration)this.props;
			}
		}

		public override void CompPostTick(ref float severityAdjustment)
		{
			base.CompPostTick(ref severityAdjustment);
			Pawn pawn = base.Pawn;
			if (ticks >= Props.Ticks)
			{
				if (pawn.IsBurning() || pawn.Dead || (Props.OrsimerRage && parent.Severity < 0.8f))
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
