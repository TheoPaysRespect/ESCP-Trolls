using Verse;

namespace ESCP_Trolls
{
    public class  HediffCompProperties_Regeneration : HediffCompProperties
	{
		public HediffCompProperties_Regeneration()
		{
			this.compClass = typeof(HediffComp_Regeneration);
		}
		public int Ticks = 100;
		public float BaseNumber = 3f;
		public float Tend = 0.2f;
		public float Severity = 0.5f;
		public bool OrsimerRage = false;
	}
}
