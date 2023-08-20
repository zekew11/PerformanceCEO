using System;
using UModFramework.API;

namespace PerformanceCEO
{
    public class PerformanceCEOConfig
    {
        private static readonly string configVersion = "1.0";
        public static bool RAMReductionModuleEnabled = true;
        public static bool VRAMReductionModuleEnabled = true;
        public static bool DebugLogs = false;

        //Add your config vars here.

        internal static void Load()
        {
            PerformanceCEO.Log("Loading settings.");
            try
            {
                using (UMFConfig cfg = new UMFConfig())
                {
                    string cfgVer = cfg.Read("ConfigVersion", new UMFConfigString());
                    if (cfgVer != string.Empty && cfgVer != configVersion)
                    {
                        cfg.DeleteConfig(false);
                        PerformanceCEO.Log("The config file was outdated and has been deleted. A new config will be generated.");
                    }

                    cfg.Write("SupportsHotLoading", new UMFConfigBool(false)); //Uncomment if your mod can't be loaded once the game has started.
                    cfg.Write("ModDependencies", new UMFConfigStringArray(new string[] { "" })); //A comma separated list of mod/library names that this mod requires to function. Format: SomeMod:1.50,SomeLibrary:0.60
                    cfg.Read("LoadPriority", new UMFConfigString("Normal"));
                    cfg.Write("MinVersion", new UMFConfigString("0.53.7"));
                    cfg.Write("MaxVersion", new UMFConfigString("0.54.99999.99999")); //This will prevent the mod from being loaded after the next major UMF release
                    cfg.Write("UpdateURL", new UMFConfigString(""));
                    cfg.Write("ConfigVersion", new UMFConfigString(configVersion));
                    cfg.Write("UpdateURL", new UMFConfigString("https://umodframework.com/updatemod?id=37"));

                    PerformanceCEO.Log("Finished UMF Settings.");

                    RAMReductionModuleEnabled = cfg.Read("RAM Usage Reduction Module", new UMFConfigBool(true, false, true), "This module reduces RAM usage while in game considerably when you have modded airlines in a save.");
                    VRAMReductionModuleEnabled = cfg.Read("VRAM Usage Reduction Module", new UMFConfigBool(true, false, true), "This module reduces VRAM usage while in game considerably.");
                    DebugLogs = cfg.Read("Log Additional Debug Information", new UMFConfigBool(false, false, false), "Makes the mod log additional information for debugging.");

                    PerformanceCEO.Log("Finished loading settings.");
                }
            }
            catch (Exception e)
            {
                PerformanceCEO.Log("Error loading mod settings: " + e.Message + "(" + e.InnerException?.Message + ")");
            }
        }
    }
}