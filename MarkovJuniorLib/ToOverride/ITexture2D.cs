namespace MarkovJuniorLib.ToOverride
{
    public interface ITexture2D
    {
        int Height { get; }
        int Width { get; }
        Color32[] GetPixels32();
        void SetPixels32(Color32[] pixels);
    }
}
