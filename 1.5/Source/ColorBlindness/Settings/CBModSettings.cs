using Verse;

namespace ColorBlindness
{
    public class CBModSettings : ModSettings
    {
        private static CBModSettings _instance;

        public bool _EnableColorBlindModes = false;
        public bool _ProtanopiaMode = false;
        public bool _ProtanomalyMode = false;
        public bool _DeuteranopiaMode = false;
        public bool _DeuteranomalyMode = false;
        public bool _TritanopiaMode = false;
        public bool _TritanomalyMode = false;
        public bool _AchromatopsiaMode = false;
        public bool _AchromatomalyMode = false;

        public CBModSettings()
        {
            _instance = this;
        }

        public static bool EnableColorBlindModes => _instance._EnableColorBlindModes;
        public static bool ProtanopiaMode => _instance._ProtanopiaMode;
        public static bool ProtanomalyMode => _instance._ProtanomalyMode;
        public static bool DeuteranopiaMode => _instance._DeuteranopiaMode;
        public static bool DeuteranomalyMode => _instance._DeuteranomalyMode;
        public static bool TritanopiaMode => _instance._TritanopiaMode;
        public static bool TritanomalyMode => _instance._TritanomalyMode;
        public static bool AchromatopsiaMode => _instance._AchromatopsiaMode;
        public static bool AchromatomalyMode => _instance._AchromatomalyMode;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref _EnableColorBlindModes, "_EnableColorBlindModes", false);

            Scribe_Values.Look(ref _ProtanopiaMode, "_ProtanopiaMode", false);
            Scribe_Values.Look(ref _ProtanomalyMode, "_ProtanomalyMode", false);
            Scribe_Values.Look(ref _DeuteranopiaMode, "_DeuteranopiaMode", false);
            Scribe_Values.Look(ref _DeuteranomalyMode, "_DeuteranomalyMode", false);
            Scribe_Values.Look(ref _TritanopiaMode, "_TritanopiaMode", false);
            Scribe_Values.Look(ref _TritanomalyMode, "_TritanomalyMode", false);
            Scribe_Values.Look(ref _AchromatopsiaMode, "_AchromatopsiaMode", false);
            Scribe_Values.Look(ref _AchromatomalyMode, "_AchromatomalyMode", false);
        }
    }   
}