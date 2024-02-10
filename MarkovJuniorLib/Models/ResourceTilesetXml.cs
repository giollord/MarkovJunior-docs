namespace MarkovJuniorLib.Models
{
    public class ResourceTilesetXml : Resource
    {
        public ResourceTilesetXml(string tilesetXml)
        {
            TilesetXml = tilesetXml;
        }

        public string TilesetXml { get; set; }
    }
}
