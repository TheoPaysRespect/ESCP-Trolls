using Verse;
using System.Collections.Generic;

namespace ESCP_Trolls
{
    public class RaceProperties : DefModExtension
    {

        public bool scaleExtraButcher = false;
        public List<ThingDef> extraButcherItems;    //specifically ones to be scaled

        public float chanceToRequire = 1f;
        public bool requireRiver = false;
        public bool requireCoast = false;
        public bool requireCaves = false;
        public bool requireHills = false;

        public static RaceProperties Get(Def def)
        {
            return def.GetModExtension<RaceProperties>();
        }
    }
}
