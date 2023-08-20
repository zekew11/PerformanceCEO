using UnityEngine;
using UModFramework.API;
using System;
using System.Linq;
using System.Collections.Generic;

namespace PerformanceCEO
{
    [UMFHarmony(3)] //Set this to the number of harmony patches in your mod.
    [UMFScript]
    class PerformanceCEO : MonoBehaviour
    {
        internal static void Log(string text, bool clean = false)
        {
            using (UMFLog log = new UMFLog()) log.Log(text, clean);
        }

        [UMFConfig]
        public static void LoadConfig()
        {
            PerformanceCEOConfig.Load();
        }

		void Awake()
		{
			Log("PerformanceCEO v" + UMFMod.GetModVersion().ToString(), true);

            GameObject child = Instantiate(new GameObject());
            child.transform.SetParent(null);
            child.name = "PerformanceCEOActive";
		}

        void Update()
        {
        }
	}
}