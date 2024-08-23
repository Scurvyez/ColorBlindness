using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Verse;

namespace ColorBlindness
{
    [StaticConstructorOnStartup]
    public static class CBContentDatabase
    {
        private static AssetBundle bundleInt;
        private static Dictionary<string, Shader> lookupShaders;
        public static readonly Shader ScreenColorBlindness = LoadShader(Path.Combine("Assets", "ScreenColorBlindness.shader"));
        
        public static AssetBundle CBBundle
        {
            get
            {
                if (bundleInt == null)
                {
                    bundleInt = ColorBlindnessMod.mod.MainBundle;
                }
                return bundleInt;
            }
        }

        private static Shader LoadShader(string shaderName)
        {
            lookupShaders ??= new Dictionary<string, Shader>();
            
            if (!lookupShaders.ContainsKey(shaderName))
            {
                lookupShaders[shaderName] = CBBundle.LoadAsset<Shader>(shaderName);
            }
            
            Shader shader = lookupShaders[shaderName];
            
            if (shader != null) return shader;
            CBLog.Warning("Could not load shader: " + shaderName);
            return ShaderDatabase.DefaultShader;
        }
    }
}
