using Godot;
using System.Linq;

namespace MarkovJuniorLibGodot
{
    public class ModelConfig : MarkovJuniorLib.Models.ModelConfigBase<Image>
    {
        protected override MarkovJuniorLib.ToOverride.ITexture2D ConvertTexture(Image tex) => tex == null ? null : new GodotTexture2D(tex);
    }

    public class GodotTexture2D : MarkovJuniorLib.ToOverride.ITexture2D
    {
        public Image Texture;

        public GodotTexture2D(Image tex)
        {
            Texture = tex;
        }

        public int Height => Texture.GetHeight();
        public int Width => Texture.GetWidth();

        public MarkovJuniorLib.Models.Color32[] GetPixels32()
        {
            var w = Width;
            var h = Height;
            var result = new MarkovJuniorLib.Models.Color32[w * h];
            for (var x = 0; x < w; x++)
                for (var y = 0; y < h; y++)
                {
                    var c = Texture.GetPixel(x, h - y - 1);
                    result[y * w + h] = new MarkovJuniorLib.Models.Color32((byte)c.R8, (byte)c.G8, (byte)c.B8, (byte)c.A8);
                }
            return result;
        }

        public void SetPixels32(MarkovJuniorLib.Models.Color32[] pixels)
        {
            var w = Width;
            var h = Height;
            for (var x = 0; x < w; x++)
                for (var y = 0; y < h; y++)
                {
                    var cur = pixels[y * w + x];
                    var c = new Color((uint)((cur.R << 24) | (cur.G << 16) | (cur.B << 8) | (cur.A)));
                    Texture.SetPixel(x, h - y - 1, c);
                }
        }
    }

    public class GodotDebugLogger : MarkovJuniorLib.ToOverride.IDebugLogger
    {
        public void Log(string message) => GD.Print(message);

        public void LogError(string message) => GD.PrintErr(message);
    }

    public class GodotMathf : MarkovJuniorLib.ToOverride.IMathf
    {
        public float Exp(float a) => Mathf.Exp(a);

        public float Log(float a) => Mathf.Log(a);

        public float Pow(float a, float pow) => Mathf.Pow(a, pow);

        public float Sqrt(float a) => Mathf.Sqrt(a);
    }

    public class GodotRandom : MarkovJuniorLib.ToOverride.IRandom
    {
        private static RandomNumberGenerator _rng;

        static GodotRandom()
        {
            _rng = new RandomNumberGenerator();
        }

        public int Range(int minInclusive, int maxExclusive) => _rng.RandiRange(minInclusive, maxExclusive - 1);
    }

    public class GodotTextureHelper : MarkovJuniorLib.ToOverride.ITextureHelper
    {
        public MarkovJuniorLib.ToOverride.ITexture2D Create(int width, int height) => new GodotTexture2D(Image.Create(width, height, false, Image.Format.Rgba8));

        public MarkovJuniorLib.ToOverride.ITexture2D ParseFromFile(byte[] bytes)
        {
            Image img = new Image();
            img.LoadPngFromBuffer(bytes);
            return new GodotTexture2D(img);
        }
    }

    public class RunResult : MarkovJuniorLib.RunResult<Image>
    {
    }
}
