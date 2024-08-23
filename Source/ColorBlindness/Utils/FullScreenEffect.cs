using UnityEngine;

namespace ColorBlindness
{
    public class FullScreenEffects : MonoBehaviour
    {
        public Material cBMMat;
        private static FullScreenEffects _instance;

        public void Start()
        {
            _instance = this;
            cBMMat = new Material(CBContentDatabase.ScreenColorBlindness);
        }
        
        private void Update()
        {
            if (!CBModSettings.EnableColorBlindModes) return;
            if (_instance is null) return;

            ColorBlindnessUtility.ColorBlindMode mode = GetActiveColorBlindMode();
            ColorBlindnessUtility.SetColorBlindnessProperties(_instance.cBMMat, mode);
        }

        private static ColorBlindnessUtility.ColorBlindMode GetActiveColorBlindMode()
        {
            return CBModSettings.ProtanopiaMode ? ColorBlindnessUtility.ColorBlindMode.Protanopia :
                CBModSettings.ProtanomalyMode   ? ColorBlindnessUtility.ColorBlindMode.Protanomaly :
                CBModSettings.DeuteranopiaMode  ? ColorBlindnessUtility.ColorBlindMode.Deuteranopia :
                CBModSettings.DeuteranomalyMode ? ColorBlindnessUtility.ColorBlindMode.Deuteranomaly :
                CBModSettings.TritanopiaMode    ? ColorBlindnessUtility.ColorBlindMode.Tritanopia :
                CBModSettings.TritanomalyMode   ? ColorBlindnessUtility.ColorBlindMode.Tritanomaly :
                CBModSettings.AchromatopsiaMode ? ColorBlindnessUtility.ColorBlindMode.Achromatopsia :
                CBModSettings.AchromatomalyMode ? ColorBlindnessUtility.ColorBlindMode.Achromatomaly :
                ColorBlindnessUtility.ColorBlindMode.Normal;
        }

        public void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (CBModSettings.EnableColorBlindModes)
            {
                Graphics.Blit(source, destination, cBMMat);
            }
            else 
            {
                Graphics.Blit(source, destination);
            }
        }
    }
}