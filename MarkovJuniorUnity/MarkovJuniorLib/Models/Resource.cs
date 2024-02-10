using MarkovJuniorLib.ToOverride;

namespace MarkovJuniorLib.Models
{
    public abstract class Resource
    {
        public static ResourceTilesetXml TilesetXml(string tilesetXml) => new ResourceTilesetXml(tilesetXml);
        public static ResourceVoxBytes Vox(byte[] voxBytes) => new ResourceVoxBytes(voxBytes);
        public static ResourceColor32Grid Color32Grid(Color32[,,] colorsGrid) => new ResourceColor32Grid(colorsGrid);
        public static ResourceTexture Texture(ITexture2D texture) => new ResourceTexture(texture);
    }
}
