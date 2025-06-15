using Verse;

namespace ESCP_Trolls
{
    public class BiomeFeatureRequirements : DefModExtension
    {
        public bool requireRiver = false;
        public bool requireCoast = false;
        public bool requireCaves = false;
        public bool requireHills = false;

        public static BiomeFeatureRequirements Get(Def def)
        {
            return def.GetModExtension<BiomeFeatureRequirements>();
        }
    }
}
