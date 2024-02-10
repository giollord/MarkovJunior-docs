using MarkovJuniorLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MarkovJuniorLibUnity
{
    public class MarkovJuniorRunner : MarkovJuniorRunner<Texture2D, UnityEngine.Color32, ModelConfig, RunResult>
    {
        private UnityRandom _random;
        private UnityTextureHelper _texHelper;

        static MarkovJuniorRunner()
        {
            MarkovJuniorLib.ToOverride.Debug.SetLogger(new UnityDebugLogger());
            MarkovJuniorLib.ToOverride.Mathf.SetInterpreter(new UnityMathf());
        }

        public MarkovJuniorRunner()
        {
            _random = new UnityRandom();
            _texHelper = new UnityTextureHelper();
        }

        public override IEnumerable<RunResult> Run(ModelConfig modelConfig)
        {
            var res = Run(modelConfig, _random, _texHelper);
            return res.Select(x => new RunResult { Texture = (x.Texture as UnityTexture2D).Texture, Vox = x.Vox });
        }

        protected override UnityEngine.Color32 ConvertColor(MarkovJuniorLib.Models.Color32 c) => new UnityEngine.Color32(c.R, c.G, c.B, c.A);
    }
}
