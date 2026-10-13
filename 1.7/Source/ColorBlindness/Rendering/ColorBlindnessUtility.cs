using ColorBlindness.Settings;
using Waylith.Math;

namespace ColorBlindness.Rendering
{
  public static class ColorBlindnessUtility
  {
    public enum ColorBlindMode
    {
      Normal = 0,
      Protanopia = 1,
      Protanomaly = 2,
      Deuteranopia = 3,
      Deuteranomaly = 4,
      Tritanopia = 5,
      Tritanomaly = 6,
      Achromatopsia = 7,
      Achromatomaly = 8,
    }
    
    public static ColorBlindMode GetActiveColorBlindMode()
    {
      return true switch
      {
        _ when CbModSettings.ProtanopiaMode => ColorBlindMode.Protanopia,
        _ when CbModSettings.ProtanomalyMode => ColorBlindMode.Protanomaly,
        _ when CbModSettings.DeuteranopiaMode => ColorBlindMode.Deuteranopia,
        _ when CbModSettings.DeuteranomalyMode => ColorBlindMode.Deuteranomaly,
        _ when CbModSettings.TritanopiaMode => ColorBlindMode.Tritanopia,
        _ when CbModSettings.TritanomalyMode => ColorBlindMode.Tritanomaly,
        _ when CbModSettings.AchromatopsiaMode => ColorBlindMode.Achromatopsia,
        _ when CbModSettings.AchromatomalyMode => ColorBlindMode.Achromatomaly,
        _ => ColorBlindMode.Normal
      };
    }
    
    public static ColorBlindnessPushConstants GetPushConstants(ColorBlindMode mode)
    {
      return mode switch
      {
        ColorBlindMode.Protanopia => new ColorBlindnessPushConstants
        {
          R = new float4(0.56667f, 0.43333f, 0f, 1f),
          G = new float4(0.55833f, 0.44167f, 0f, 1f),
          B = new float4(0f, 0.24167f, 0.75833f, 1f)
        },
        
        ColorBlindMode.Protanomaly => new ColorBlindnessPushConstants
        {
          R = new float4(0.81667f, 0.18333f, 0f, 1f),
          G = new float4(0.33333f, 0.66667f, 0f, 1f),
          B = new float4(0f, 0.125f, 0.875f, 1f)
        },
        
        ColorBlindMode.Deuteranopia => new ColorBlindnessPushConstants
        {
          R = new float4(0.625f, 0.375f, 0f, 1f),
          G = new float4(0.7f, 0.3f, 0f, 1f),
          B = new float4(0f, 0.3f, 0.7f, 1f)
        },
        
        ColorBlindMode.Deuteranomaly => new ColorBlindnessPushConstants
        {
          R = new float4(0.8f, 0.2f, 0f, 1f),
          G = new float4(0f, 0.25833f, 0.74167f, 1f),
          B = new float4(0f, 0.14167f, 0.85833f, 1f)
        },
        
        ColorBlindMode.Tritanopia => new ColorBlindnessPushConstants
        {
          R = new float4(0.95f, 0.05f, 0f, 1f),
          G = new float4(0f, 0.43333f, 0.56667f, 1f),
          B = new float4(0f, 0.475f, 0.525f, 1f)
        },
        
        ColorBlindMode.Tritanomaly => new ColorBlindnessPushConstants
        {
          R = new float4(0.96667f, 0.03333f, 0f, 1f),
          G = new float4(0f, 0.73333f, 0.26667f, 1f),
          B = new float4(0f, 0.18333f, 0.81667f, 1f)
        },
        
        ColorBlindMode.Achromatopsia => new ColorBlindnessPushConstants
        {
          R = new float4(0.299f, 0.587f, 0.114f, 1f),
          G = new float4(0.229f, 0.587f, 0.114f, 1f),
          B = new float4(0.299f, 0.587f, 0.114f, 1f)
        },
        
        ColorBlindMode.Achromatomaly => new ColorBlindnessPushConstants
        {
          R = new float4(0.618f, 0.32f, 0.062f, 1f),
          G = new float4(0.163f, 0.775f, 0.062f, 1f),
          B = new float4(0.163f, 0.32f, 0.516f, 1f)
        },
        
        _ => new ColorBlindnessPushConstants
        {
          R = new float4(1f, 0f, 0f, 1f),
          G = new float4(0f, 1f, 0f, 1f),
          B = new float4(0f, 0f, 1f, 1f)
        }
      };
    }
  }
}