namespace MarkovJuniorLib.Models
{
    public class ResourceColor32Grid : Resource
    {
        public ResourceColor32Grid(Color32[,,] colorsGrid)
        {
            ColorsGrid = colorsGrid;
        }

        public Color32[,,] ColorsGrid { get; set; }
    }
}
