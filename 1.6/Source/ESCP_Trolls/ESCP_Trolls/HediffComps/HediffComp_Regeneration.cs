using Verse;
using RimWorld;

namespace ESCP_Trolls
{
    public class HediffComp_Regeneration : HediffComp
	{

		public HediffCompProperties_Regeneration Props => (HediffCompProperties_Regeneration)props;

        public override void CompPostTickInterval(ref float severityAdjustment, int delta)
        {
            base.CompPostTickInterval(ref severityAdjustment, delta);
            if (Pawn.IsHashIntervalTick(Props.Ticks, delta))
            {
                if (Pawn.IsBurning() || Pawn.Dead || (Props.OrsimerRage && parent.Severity < 0.8f))
                {
                    return;
                }
                else
                {
                    Utility.HealWounds(Pawn, Props.BaseNumber, Props.Severity, Props.Tend);
                }
            }
        }
    }
}
