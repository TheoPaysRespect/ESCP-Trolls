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
    /// Currently breaks if Geological Landforms is loaded, but not my problem as that will also breaks pollution animal spawns
    /// </summary>
    [HarmonyPatch(typeof(WildAnimalSpawner))]
    [HarmonyPatch("CommonalityOfAnimalNow")]
    public static class WildAnimalSpawner_CommonalityOfAnimalNow_Patch
    {
        [HarmonyPostfix]
        public static void TrollFeatureRequirementsPatch(PawnKindDef def, ref Map ___map, ref float __result)
        {
            if (Trolls_ModSettings.TileFeatureRequirement)
            {
                BiomeFeatureRequirements props = BiomeFeatureRequirements.Get(def.race);
                if (props != null)
                {
                    if (props.requireCaves && !Find.World.HasCaves(___map.Tile))
                    {
                        __result = 0;
                        return;
                    }
                    if (props.requireCoast && !Find.World.CoastDirectionAt(___map.Tile).IsValid)
                    {
                        __result = 0;
                        return;
                    }
                    if (props.requireHills && Find.WorldGrid[___map.Tile].hilliness == Hilliness.Flat)
                    {
                        __result = 0;
                        return;
                    }
                    if (props.requireRiver && Find.WorldGrid[___map.Tile].Rivers == null)
                    {
                        __result = 0;
                        return;
                    }
                }
            }
        }
    }

    /* Alternative, but ticks fairly frequently
    [HarmonyPatch(typeof(MapTemperature))]
    [HarmonyPatch("SeasonAcceptableFor")]
    public static class MapTemperature_SeasonAcceptableFor_Patch
    {
        [HarmonyPostfix]
        public static void TrollFeatureRequirementsPatch(ThingDef animalRace, ref Map ___map, ref bool __result)
        {
            if (__result && Trolls_ModSettings.TileFeatureRequirement)
            {
                BiomeFeatureRequirements props = BiomeFeatureRequirements.Get(animalRace);
                if (props != null)
                {
                    if (props.requireCaves && !Find.World.HasCaves(___map.Tile))
                    {
                        __result = false;
                        return;
                    }
                    if (props.requireCoast && !Find.World.CoastDirectionAt(___map.Tile).IsValid)
                    {
                        __result = false;
                        return;
                    }
                    if (props.requireHills && Find.WorldGrid[___map.Tile].hilliness == Hilliness.Flat)
                    {
                        __result = false;
                        return;
                    }
                    if (props.requireRiver && Find.WorldGrid[___map.Tile].Rivers == null)
                    {
                        __result = false;
                        return;
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
                __result *= Trolls_ModSettings.TrollCommonalityMult;
            }
        }
    }
}