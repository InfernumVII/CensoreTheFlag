using System.Collections.Generic;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CensoreTheFlag
{
    [BepInPlugin("com.infernumvii.censoretheflag", "Censore The Flag", "1.0.0")]
    public class CensoreTheFlag : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("com.infernumvii.censoretheflag");
        internal static new ManualLogSource Logger;

        private void Awake()
        {
            Logger = base.Logger;
            harmony.PatchAll();
            Logger.LogInfo("Censore The Flag loaded!");
        }

        
        
        [HarmonyPatch(typeof(MainMenuManager))]
        class FlagMaterialReplacePatch
        {
            [HarmonyPatch("ActuallyStartGameActually")]
            [HarmonyPostfix]
            static void Postfix(MainMenuManager __instance)
            {

                FlagController[] flags = GameObject.FindObjectsOfType<FlagController>(true);
                var blankMaterial = flags[0].flagMats[0];
                foreach (FlagController flag in flags)
                {
                    flag.flagvisual.material = blankMaterial;
                    flag.flagMats[1] = blankMaterial;
                    flag.flagMats[2] = blankMaterial;
                    Logger.LogInfo("Flag replaced");
                }
            }
        }
        
    }
}