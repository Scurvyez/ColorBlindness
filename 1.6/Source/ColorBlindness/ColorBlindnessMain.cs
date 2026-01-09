using HarmonyLib;
using Verse;

namespace ColorBlindness
{
    [StaticConstructorOnStartup]
    public static class ColorBlindnessMain
    {
        static ColorBlindnessMain()
        {
            Harmony harmony = new ("com.colorblindness");
            harmony.PatchAll();
        }
    }
}