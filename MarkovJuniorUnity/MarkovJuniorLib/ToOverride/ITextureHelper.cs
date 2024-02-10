namespace MarkovJuniorLib.ToOverride
{
    public interface ITextureHelper
    {
        ITexture2D Create(int width, int height);
        ITexture2D ParseFromFile(byte[] bytes);
    }
}
