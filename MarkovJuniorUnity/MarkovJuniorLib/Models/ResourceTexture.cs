using MarkovJuniorLib.ToOverride;

namespace MarkovJuniorLib.Models
{
    public class ResourceTexture : Resource
    {
        public ResourceTexture(ITexture2D texture)
        {
            Texture = texture;
        }

        public new ITexture2D Texture { get; set; }
    }
}
