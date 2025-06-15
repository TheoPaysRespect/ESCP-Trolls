using Verse;
using RimWorld;

namespace ESCP_Trolls
{
    public class Comp_Regeneration : ThingComp
	{
		public CompProperties_Regeneration Props => (CompProperties_Regeneration)props;

        public override void CompTickInterval(int delta)
        {
            base.CompTickInterval(delta);
            Pawn pawn = parent as Pawn;
            if (pawn.IsHashIntervalTick(Props.Ticks, delta))
            {
                if (pawn.IsBurning() || pawn.Dead)
                {
                    return;
                }
                else
                {
                    Utility.HealWounds(pawn, Props.BaseNumber, Props.Severity, Props.Tend);
                }
            }
        }
    }
}
