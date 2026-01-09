using UnityEngine;
using Verse;
using HarmonyLib;
using System.Runtime.InteropServices;
using System.IO;

namespace ColorBlindness
{
    public class ColorBlindnessMod : Mod
    {
        public static ColorBlindnessMod mod;
        CBModSettings settings;
        
        public ColorBlindnessMod(ModContentPack content) : base(content)
        {
            mod = this;
            settings = GetSettings<CBModSettings>();

            Harmony harmony = new ("com.colorblindness");

            harmony.Patch(original: AccessTools.PropertyGetter(typeof(ShaderTypeDef), nameof(ShaderTypeDef.Shader)),
                prefix: new HarmonyMethod(typeof(ColorBlindnessMod),
                nameof(ShaderFromAssetBundle)));

            harmony.PatchAll();
        }

        public static void ShaderFromAssetBundle(ShaderTypeDef __instance, ref Shader ___shaderInt)
        {
            if (__instance is not CBShaderTypeDef) return;
            ___shaderInt = CBContentDatabase.CBBundle.LoadAsset<Shader>(__instance.shaderPath);
                
            if (___shaderInt is null)
            {
                CBLog.Error($"Failed to load Shader from path <text>\"{__instance.shaderPath}\"</text>");
            }
        }

        public AssetBundle MainBundle
        {
            get
            {
                string text = "";
                
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    text = "StandaloneOSX";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    text = "StandaloneWindows64";
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    text = "StandaloneLinux64";
                }
                
                string bundlePath = Path.Combine(base.Content.RootDir, "Materials\\Bundles\\" + text + "\\colorblindnessbundle");
                AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);

                if (bundle == null)
                {
                    CBLog.Error("Failed to load bundle at path: " + bundlePath);
                }
                return bundle;
            }
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            Listing_Standard list = new ();
            Rect viewRect = new (inRect.x, inRect.y, inRect.width, inRect.height);
            list.Begin(viewRect);

            list.CheckboxLabeled("CB_EnableColorBlindMode".Translate(), ref settings._EnableColorBlindModes, "CB_EnableColorBlindModeDesc".Translate());

            DrawSettingWithTexture(list, "CB_SettingProtanopia".Translate(), ref settings._ProtanopiaMode, TexButtons.ProtanopiaMode);
            DrawSettingWithTexture(list, "CB_SettingProtanomaly".Translate(), ref settings._ProtanomalyMode, TexButtons.ProtanomalyMode);
            DrawSettingWithTexture(list, "CB_SettingDeuteranopia".Translate(), ref settings._DeuteranopiaMode, TexButtons.DeuteranopiaMode);
            DrawSettingWithTexture(list, "CB_SettingDeuteranomaly".Translate(), ref settings._DeuteranomalyMode, TexButtons.DeuteranomalyMode);
            DrawSettingWithTexture(list, "CB_SettingTritanopia".Translate(), ref settings._TritanopiaMode, TexButtons.TritanopiaMode);
            DrawSettingWithTexture(list, "CB_SettingTritanomaly".Translate(), ref settings._TritanomalyMode, TexButtons.TritanomalyMode);
            DrawSettingWithTexture(list, "CB_SettingAchromatopsia".Translate(), ref settings._AchromatopsiaMode, TexButtons.AchromatopsiaMode);
            DrawSettingWithTexture(list, "CB_SettingAchromatomaly".Translate(), ref settings._AchromatomalyMode, TexButtons.AchromatomalyMode);

            list.End();
        }

        private static void DrawSettingWithTexture(Listing_Standard list, string label, ref bool value, Texture2D texture)
        {
            Rect rect = list.GetRect(24f);
            Widgets.Label(rect.LeftPartPixels(30f), new GUIContent(texture)); // draw our texture
            Widgets.CheckboxLabeled(rect.RightPartPixels(rect.width - 30f), label, ref value); // draw our checkbox with label
        }

        public override string SettingsCategory()
        {
            return "CB_ModName".Translate();
        }
    }
}
