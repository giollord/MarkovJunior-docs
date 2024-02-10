using Godot;
using MarkovJuniorLib;

namespace MarkovJuniorLibGodot
{
    public class MarkovJuniorRunner : MarkovJuniorRunnerBase<Image, Color, ModelConfig, RunResult>
    {
        private GodotRandom _random;
        private GodotTextureHelper _texHelper;

        static MarkovJuniorRunner()
        {
            MarkovJuniorLib.ToOverride.Debug.SetLogger(new GodotDebugLogger());
            MarkovJuniorLib.ToOverride.Mathf.SetInterpreter(new GodotMathf());
        }

        public MarkovJuniorRunner()
        {
            _random = new GodotRandom();
            _texHelper = new GodotTextureHelper();
        }

        public override IEnumerable<RunResult> Run(ModelConfig modelConfig)
        {
            var res = Run(modelConfig, _random, _texHelper);
            return res.Select(x => new RunResult { Texture = (x.Texture as GodotTexture2D)!.Texture, Vox = x.Vox });
        }

        protected override Color ConvertColor(MarkovJuniorLib.Models.Color32 c) => new Color((uint)((c.R << 24) | (c.G << 16) | (c.B << 8) | (c.A)));
    }
}
