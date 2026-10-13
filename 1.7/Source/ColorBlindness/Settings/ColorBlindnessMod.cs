using ColorBlindness.Rendering;
using ColorBlindness.UI;
using UnityEngine;
using Verse;

namespace ColorBlindness.Settings
{
  public class ColorBlindnessMod : Mod
  {
    private readonly CbModSettings _settings;
    
    private float _halfWidth;
    private Vector2 _leftScrollPos = Vector2.zero;
    
    private const int RowPadding = 30;
    private const float MainListingGap = 60f;
    private const float NewSectionGap = 12f;
    private const float SectionTitleGap = 6f;
    private const float Spacing = 2f;
    private const float SliderHeight = 30f;
    
    public override string SettingsCategory() => "CB_ModName".Translate();
    
    public ColorBlindnessMod(ModContentPack content) : base(content)
    {
      _settings = GetSettings<CbModSettings>();
      ColorBlindnessRenderer.Initialize();
    }
    
    public override void DoSettingsWindowContents(Rect inRect)
    {
      _halfWidth = (inRect.width - RowPadding) / 2f;
      
      Rect leftRect = new(inRect.x, inRect.y, _halfWidth, inRect.height);
      
      LeftSideScrollViewHandler(leftRect);
    }
    
    private void LeftSideScrollViewHandler(Rect inRect)
    {
      Listing_Standard leftList = new();
      leftList.Gap(MainListingGap);
      Rect viewRect1 = new(inRect.x, inRect.y, inRect.width, inRect.height);
      Rect vROffset1 = new(0, 0, inRect.width - 20, inRect.height);
      
      Widgets.BeginScrollView(viewRect1, ref _leftScrollPos, vROffset1);
      leftList.Begin(vROffset1);
      leftList.Gap(NewSectionGap);
      
      leftList.CheckboxLabeled("CB_EnableColorBlindMode".Translate(), ref _settings._EnableColorBlindModes,
        "CB_EnableColorBlindModeDesc".Translate());
      
      DrawSettingWithTexture(leftList, "CB_SettingProtanopia".Translate(), ref _settings._ProtanopiaMode,
        TexButtons.ProtanopiaMode);
      DrawSettingWithTexture(leftList, "CB_SettingProtanomaly".Translate(), ref _settings._ProtanomalyMode,
        TexButtons.ProtanomalyMode);
      DrawSettingWithTexture(leftList, "CB_SettingDeuteranopia".Translate(), ref _settings._DeuteranopiaMode,
        TexButtons.DeuteranopiaMode);
      DrawSettingWithTexture(leftList, "CB_SettingDeuteranomaly".Translate(), ref _settings._DeuteranomalyMode,
        TexButtons.DeuteranomalyMode);
      DrawSettingWithTexture(leftList, "CB_SettingTritanopia".Translate(), ref _settings._TritanopiaMode,
        TexButtons.TritanopiaMode);
      DrawSettingWithTexture(leftList, "CB_SettingTritanomaly".Translate(), ref _settings._TritanomalyMode,
        TexButtons.TritanomalyMode);
      DrawSettingWithTexture(leftList, "CB_SettingAchromatopsia".Translate(), ref _settings._AchromatopsiaMode,
        TexButtons.AchromatopsiaMode);
      DrawSettingWithTexture(leftList, "CB_SettingAchromatomaly".Translate(), ref _settings._AchromatomalyMode,
        TexButtons.AchromatomalyMode);
      
      leftList.End();
      Widgets.EndScrollView();
    }
    
    private static void DrawSettingWithTexture(Listing_Standard list, string label, ref bool value, Texture2D texture)
    {
      Rect rect = list.GetRect(24f);
      Widgets.Label(rect.LeftPartPixels(30f), new GUIContent(texture));
      Widgets.CheckboxLabeled(rect.RightPartPixels(rect.width - 30f), label, ref value);
    }
  }
}