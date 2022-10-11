using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Reflection;
using Verse;
using System;
using System.Linq;
using System.Collections.Generic;
using Verse.AI;
using Verse.AI.Group;
using System.Text;
using UnityEngine;
using RimWorld.QuestGen;
using Verse.Grammar;

namespace ESCP_Trolls
{
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.SirMashedPotato.ESCP.Trolls");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }


    /// <summary>
    /// Modifies the stack size of extra butcher products based on lifestage
    /// </summary>
    [HarmonyPatch(typeof(Thing))]
    [HarmonyPatch("ButcherProducts")]
    public static class Thing_ButcherProducts_Patch
    {
        [HarmonyPostfix]
        public static void StrangButcherPatch(ref Thing __instance, ref IEnumerable<Thing> __result)
        {
            if (__instance is Pawn && __instance.def.butcherProducts != null)
            {
                Pawn pawn = __instance as Pawn;
                var props = RaceProperties.Get(pawn.def);
                if (props != null && props.scaleExtraButcher && !props.extraButcherItems.NullOrEmpty())
                {
                    int div;
                    switch (pawn.ageTracker.CurLifeStageIndex)
                    {
                        case 0:
                            div = 3;
                            break;
                        case 1:
                            div = 2;
                            break;
                        default:
                            return;
                    }
                    List<Thing> newList = new List<Thing>();
                    foreach (Thing thing in __result)
                    {
                        if (props.extraButcherItems.Contains(thing.def))
                        {
                            int count = thing.stackCount / div;
                            Thing newThing = ThingMaker.MakeThing(thing.def, null);
                            newThing.stackCount = count;
                            newList.Insert(newList.Count, newThing);
                        }
                    }
                    __result = newList;
                }
            }
        }
    }

    /// <summary>
    /// Limit certain trolls to tiles with specific landmarks
    /// </summary>
    [HarmonyPatch(typeof(WildAnimalSpawner))]
    [HarmonyPatch("CommonalityOfAnimalNow")]
    public static class WildAnimalSpawner_CommonalityOfAnimalNow_Patch
    {
        [HarmonyPostfix]
        public static void TrollFeatureRequirementsPatch(PawnKindDef def, ref Map ___map, ref float __result)
        {
            BiomeFeatureRequirements props = BiomeFeatureRequirements.Get(def.race);
            if (props != null)
            {
                Log.Message("Checking requirements for: " + def.race.label);
                    if (props.requireCaves && !Find.World.HasCaves(___map.Tile))
                    {
                        Log.Message("Disallowing spawn:  No caves");
                        __result = 0;
                        return;
                    }
                    if (props.requireCoast && !Find.World.CoastDirectionAt(___map.Tile).IsValid)
                    {
                        Log.Message("Disallowing spawn:  No coast");
                        __result = 0;
                        return;
                    }
                    if (props.requireHills && Find.WorldGrid[___map.Tile].hilliness == Hilliness.Flat)
                    {
                        Log.Message("Disallowing spawn:  No hills");
                        __result = 0;
                        return;
                    }
                if (props.requireRiver && Find.WorldGrid[___map.Tile].Rivers == null)
                {
                    Log.Message("Disallowing spawn:  No rivers");
                    __result = 0;
                    return;
                }
            }
        }
    }
    /*
    [HarmonyPatch(typeof(MapTemperature))]
    [HarmonyPatch("SeasonAcceptableFor")]
    public static class MapTemperature_SeasonAcceptableFor_Patch
    {
        [HarmonyPostfix]
        public static void TrollFeatureRequirementsPatch(ThingDef animalRace, ref Map ___map, ref bool __result)
        {
            if (__result)
            {
                BiomeFeatureRequirements props = BiomeFeatureRequirements.Get(animalRace);
                if (props != null)
                {
                    Log.Message("Checking requirements for: " + animalRace.label);
                    if (Rand.Chance(props.chanceToRequire))
                    {
                        if (props.requireCaves && !Find.World.HasCaves(___map.Tile))
                        {
                            Log.Message("Disallowing spawn:  No caves");
                            __result = false;
                            return;
                        }
                        if (props.requireCoast && !Find.World.CoastDirectionAt(___map.Tile).IsValid)
                        {
                            Log.Message("Disallowing spawn:  No coast");
                            __result = false;
                            return;
                        }
                        if (props.requireHills && Find.WorldGrid[___map.Tile].hilliness == Hilliness.Flat)
                        {
                            Log.Message("Disallowing spawn:  No hills");
                            __result = false;
                            return;
                        }
                        if (props.requireRiver && Find.WorldGrid[___map.Tile].Rivers == null)
                        {
                            Log.Message("Disallowing spawn:  No rivers");
                            __result = false;
                            return;
                        }
                    }
                    else
                    {
                        Log.Message("Disable requirements by random chance");
                    }
                }
            }
        }
    }
    */
    /// <summary>
    /// Modifies the commonality of trolls based on a slider in the settings window
    /// </summary>
    [HarmonyPatch(typeof(BiomeDef))]
    [HarmonyPatch("CommonalityOfAnimal")]
    public static class BiomeDef_CommonalityOfAnimal_Patch
    {
        [HarmonyPostfix]
        public static void TrollCommonalityPatch(PawnKindDef animalDef, ref float __result)
        {
            if (Utility_OnStartup.trollKindDefs.Contains(animalDef))
            {
                __result *= settings;
            }
        }
    }
}