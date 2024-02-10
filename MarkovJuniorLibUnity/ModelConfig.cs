using System.Linq;
using UnityEngine;

namespace MarkovJuniorLibUnity
{
    public class ModelConfig : MarkovJuniorLib.Models.ModelConfigBase<Texture2D>
    {
        protected override MarkovJuniorLib.ToOverride.ITexture2D ConvertTexture(Texture2D tex) => tex == null ? null : new UnityTexture2D(tex);
    }

    public class UnityTexture2D : MarkovJuniorLib.ToOverride.ITexture2D
    {
        public Texture2D Texture;

        public UnityTexture2D(Texture2D tex)
        {
            Texture = tex;
        }

        public int Height => Texture.height;
        public int Width => Texture.width;

        public MarkovJuniorLib.Models.Color32[] GetPixels32() => Texture.GetPixels32().Select(x => new MarkovJuniorLib.Models.Color32(x.r, x.g, x.b, x.a)).ToArray();

        public void SetPixels32(MarkovJuniorLib.Models.Color32[] pixels)
        {
            Texture.SetPixels32(pixels.Select(x => new UnityEngine.Color32(x.R, x.G, x.B, x.A)).ToArray());
            Texture.Apply(false);
        }
    }

    public class UnityDebugLogger : MarkovJuniorLib.ToOverride.IDebugLogger
    {
        public void Log(string message) => Debug.Log(message);

        public void LogError(string message) => Debug.LogError(message);
    }

    public class UnityMathf : MarkovJuniorLib.ToOverride.IMathf
    {
        public float Exp(float a) => Mathf.Exp(a);

        public float Log(float a) => Mathf.Log(a);

        public float Pow(float a, float pow) => Mathf.Pow(a, pow);

        public float Sqrt(float a) => Mathf.Sqrt(a);
    }

    public class UnityRandom : MarkovJuniorLib.ToOverride.IRandom
    {
        public int Range(int minInclusive, int maxExclusive) => UnityEngine.Random.Range(minInclusive, maxExclusive);
    }

    public class UnityTextureHelper : MarkovJuniorLib.ToOverride.ITextureHelper
    {
        public MarkovJuniorLib.ToOverride.ITexture2D Create(int width, int height) => new UnityTexture2D(new Texture2D(width, height));

        public MarkovJuniorLib.ToOverride.ITexture2D ParseFromFile(byte[] bytes)
        {
            var tex = new Texture2D(2, 2);
            ImageConversion.LoadImage(tex, bytes);
            return new UnityTexture2D(tex);
        }
    }

    public class RunResult : MarkovJuniorLib.RunResult<Texture2D>
    {
    }
}
