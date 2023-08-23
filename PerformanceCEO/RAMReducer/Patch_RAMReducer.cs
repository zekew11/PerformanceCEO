using UnityEngine;
using HarmonyLib;
using System;
using System.Reflection;
using UModFramework;

namespace PerformanceCEO.RAMReducer
{

    public static class RAMReducerManager
    {
        public static bool LiveryImporterCall = false;
        public static bool TweaksAircraftCall = false; //set by tweaks
    }

    [HarmonyPatch(typeof(LiveryImporter))]
    [HarmonyPatch("LoadCustomLivery")]
    static class Patch_RAMReducerChecker
    {
        public static void Prefix()
        { 
            RAMReducerManager.LiveryImporterCall = true;
        }

        public static void Postfix() { RAMReducerManager.LiveryImporterCall = false; }
    }

    [HarmonyPatch(typeof(Sprite), new Type[] { typeof(Texture2D), typeof(Rect), typeof(Vector2), typeof(float), typeof(uint), typeof(SpriteMeshType)})]
    [HarmonyPatch("Create")]
    static class Patch_RAMReducerApplier
    {
        public static void Prefix(ref Texture2D texture)
        {
            if ((RAMReducerManager.LiveryImporterCall && RAMReducerManager.TweaksAircraftCall == false) || !texture.isReadable || !PerformanceCEOConfig.RAMReductionModuleEnabled)
            {
                return;
            }

            try
            {
                // This does prevent editing of the sprite later on, shouldn't be an issue
                texture.Apply(false, true);
            }
            catch (Exception ex)
            {
                PerformanceCEO.Log($"Error occured while reducing RAM usage (Patch_RAMReducerApplier). Error: {ex.Message}");
            }
        }
    }  
}