using UnityEngine;
using Verse;

namespace ColorBlindness
{
    public static class CBLog
    {
        public static Color ErrorMsgCol = new(0.4f, 0.54902f, 1.0f);
        public static Color WarningMsgCol = new(0.70196f, 0.4f, 1.0f);
        public static Color MessageMsgCol = new(0.4f, 1.0f, 0.54902f);

        public static void Error(string msg)
        {
            Log.Error("[Color Blindness] ".Colorize(ErrorMsgCol) + msg);
        }

        public static void Warning(string msg)
        {
            Log.Warning("[Color Blindness] ".Colorize(WarningMsgCol) + msg);
        }

        public static void Message(string msg)
        {
            Log.Message("[Color Blindness] ".Colorize(MessageMsgCol) + msg);
        }
    }
}