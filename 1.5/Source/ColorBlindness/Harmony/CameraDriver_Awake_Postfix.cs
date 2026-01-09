using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace ColorBlindness
{
    [StaticConstructorOnStartup]
    [HarmonyPatch(typeof(CameraDriver), "Awake")]
    public class CameraDriver_Awake_Postfix
    {
        public static void Postfix(CameraDriver __instance)
        {
            if (__instance == null) return;

            PropertyInfo myCameraProperty = AccessTools.Property(typeof(CameraDriver), "MyCamera");
            if (myCameraProperty == null) return;

            Camera myCamera = (Camera)myCameraProperty.GetValue(__instance);
            if (myCamera == null)  return;

            if (myCamera.gameObject.GetComponent<FullScreenEffects>() != null) return;
            myCamera.gameObject.AddComponent<FullScreenEffects>();
        }
    }
}