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
        private static Dictionary<string, Shader> _lookupShaders;
        public static readonly Shader ScreenColorBlindness = LoadShader(Path.Combine("Assets", "ScreenColorBlindness.shader"));
        
        public static AssetBundle CbBundle
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
            _lookupShaders ??= new Dictionary<string, Shader>();
            
            if (!_lookupShaders.ContainsKey(shaderName))
            {
                _lookupShaders[shaderName] = CbBundle.LoadAsset<Shader>(shaderName);
            }
            
            Shader shader = _lookupShaders[shaderName];
            
            if (shader != null) return shader;
            CBLog.Warning("Could not load shader: " + shaderName);
            return ShaderDatabase.DefaultShader;
        }
    }
}
