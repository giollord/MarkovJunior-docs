namespace MarkovJuniorLibUnity
{
    public static class ExtensionMethods
    {
        public static UnityEngine.Color32 ToUnityColor32(this MarkovJuniorLib.Models.Color32 color) => new UnityEngine.Color32(color.R, color.G, color.B, color.A);
        public static MarkovJuniorLib.Models.Color32 ToMarkovJuniorColor32(this UnityEngine.Color32 color) => new MarkovJuniorLib.Models.Color32(color.r, color.g, color.b, color.a);
    }
}
