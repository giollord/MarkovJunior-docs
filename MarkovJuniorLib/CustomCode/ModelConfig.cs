using System.Collections.Generic;
using UnityEngine;

namespace MarkovJuniorLib
{
    public class ModelConfig
    {
        public string Name { get; set; }
        public int Size { get; set; } = -1;
        public int Dimension { get; set; } = 2;
        public int MX { get; set; } = -1;
        public int MY { get; set; } = -1;
        public int MZ { get; set; } = 1;
        public int Amount { get; set; } = 2;
        public int PixelSize { get; set; } = 4;
        public int[] Seeds { get; set; } = null;
        public bool Gif { get; set; } = false;
        public bool Iso { get; set; } = false;
        public int Steps { get; set; } = 50000;
        public int Gui { get; set; } = 0;
        public ModelColor[] Colors { get; set; } = new ModelColor[0];
        public string ModelXML { get; set; } = null;
        public Texture2D InitialTexture { get; set; } = null;
        public Dictionary<string, Texture2D> Samples { get; set; } = new();
        public Dictionary<string, string> TilesetXmls { get; set; } = new();
        public Dictionary<string, byte[]> Resources { get; set; } = new();

        public void SetSize(int size)
        {
            Size = size;
            MX = size;
            MY = size;
        }
    }

    public class ModelColor
    {
        public char Symbol { get; set; }
        public Color32 Color { get; set; }
        public string Comment { get; set; }
    }
}
