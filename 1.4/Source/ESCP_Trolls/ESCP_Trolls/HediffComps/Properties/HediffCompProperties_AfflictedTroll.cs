using Verse;

namespace ESCP_Trolls
{
    public class HediffCompProperties_AfflictedTroll : HediffCompProperties
    {
        public HediffCompProperties_AfflictedTroll()
        {
            this.compClass = typeof(HediffComp_AfflictedTroll);
        }

        public bool questEnd = true;
    }
}
