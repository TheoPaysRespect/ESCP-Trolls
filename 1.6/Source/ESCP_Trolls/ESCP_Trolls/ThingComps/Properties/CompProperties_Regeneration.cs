using Verse;

namespace ESCP_Trolls
{
    public class CompProperties_Regeneration : CompProperties
	{
		public CompProperties_Regeneration()
		{
			this.compClass = typeof(Comp_Regeneration);
		}
		public int Ticks = 100;
		public float BaseNumber = 5f;
		public float Tend = 0.2f;
		public float Severity = 0.5f;
	}
}