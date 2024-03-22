using Verse;

namespace ESCP_Trolls
{
    public class Trolls_ModSettings : ModSettings
    {
        private static Trolls_ModSettings _instance;

        //getters
        public static bool EnableTrollHuntQuest => _instance.enableAnimaTrollIncident;
        public static bool EnableAnimaTrollIncident => _instance.enableAnimaTrollIncident;
        public static bool TileFeatureRequirement => _instance.tileFeatureRequirement;
        public static float TrollCommonalityMult => _instance.trollCommonalityMult;

        //settings
        public bool enableTrollHuntQuest = true;
        public bool enableAnimaTrollIncident = true;
        public bool tileFeatureRequirement = true;
        public float trollCommonalityMult = 1f;

        public Trolls_ModSettings()
        {
            _instance = this;
        }

        //save

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref enableTrollHuntQuest, "enableTrollHuntQuest", true);
            Scribe_Values.Look(ref enableAnimaTrollIncident, "enableAnimaTrollIncident", true);
            Scribe_Values.Look(ref tileFeatureRequirement, "tileFeatureRequirement", true);
            Scribe_Values.Look(ref trollCommonalityMult, "trollCommonalityMult", 1f);
        }

        //reset

        public void ResetSettings()
        {
            _instance.enableTrollHuntQuest = true;
            _instance.enableAnimaTrollIncident = true;
            _instance.tileFeatureRequirement = true;
            _instance.trollCommonalityMult = 1f;
        }
    }
}
