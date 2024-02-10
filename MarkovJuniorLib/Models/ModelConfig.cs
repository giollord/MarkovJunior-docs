using MarkovJuniorLib.Internal;
using MarkovJuniorLib.ToOverride;
using System.Collections.Generic;
using System.Linq;

namespace MarkovJuniorLib.Models
{
    /// <summary>
    /// Configuration to run model
    /// </summary>
    public abstract class ModelConfig
    {
        /// <summary>
        /// XML of model to run. Required
        /// </summary>
        public string ModelXML = null;
        /// <summary>
        /// Width of output. Required
        /// </summary>
        public int Width_MX = -1;
        /// <summary>
        /// Height of output. Required
        /// </summary>
        public int Height_MY = -1;
        /// <summary>
        /// Depth of input. If set to 1, it will mean 2D model. Default is 1
        /// </summary>
        public int Depth_MZ = 1;
        /// <summary>
        /// Independent generator runs count. Default is 1
        /// </summary>
        public int Amount = 1;
        /// <summary>
        /// Size of pixel of output texture. So, if set to 4 with 10x20 field, it will return 40x80 texture. Default is 1
        /// </summary>
        public int PixelSize = 1;
        /// <summary>
        /// Array of seeds for each generator run. If it's smaller than <see cref="Amount"/>, random seeds will be used. Default is null
        /// </summary>
        public int[] Seeds = null;
        /// <summary>
        /// If set to true, then output will contain all generator steps; if set to false single image otherwise. Default is false
        /// </summary>
        public bool Gif = false;
        /// <summary>
        /// If set to true, it will generate isometric output image. Default is false
        /// </summary>
        public bool Iso = false;
        /// <summary>
        /// If set to true, then output will contain 2d texture if Depth_MZ is set to 1
        /// </summary>
        public bool Output2dTexture = true;
        /// <summary>
        /// If set to true, then output will be encoded as .vox file
        /// </summary>
        public bool Output3dVox = false;
        /// <summary>
        /// If set to true, then output will be encoded as Color32[x,y,z] array
        /// </summary>
        public bool Output3dColors = false;
        /// <summary>
        /// Maximum amount of generator steps. Default is 50000
        /// </summary>
        public int Steps = 50000;
        /// <summary>
        /// Used when <see cref="Gui"/> is set to render on GUI. Default is empty string
        /// </summary>
        public string Name = string.Empty;
        /// <summary>
        /// Size of GUI. Usually either 0 (no GUI), either 150 (used in original MarkovJunior repository). Default is 0
        /// </summary>
        public int Gui = 0;
        /// <summary>
        /// Custom color pallette. Default is empty array
        /// </summary>
        public ModelColor[] Colors = new ModelColor[0];
        /// <summary>
        /// Optional initial grid provided to generator. Must contain only default and custom defined colors. Default is null
        /// </summary>
        internal abstract Color32[,,] InitialGridInternal { get; }
        ///// <summary>
        ///// Tileset and tiles XML configurations. Key is tileset name, value is tileset configuration XML. Key is tileset name or tile configuration in format "TILESETNAME/TILENAME", value is tileset or tile XML. Default is empty dictionary
        ///// </summary>
        //public Dictionary<string, string> TilesetXmls = new();
        ///// <summary>
        ///// Contents of .vox files to use with "sample", "fin", "fout" and "file" attributes. Key is resource name, value is .vox file. Default is empty dictionary
        ///// </summary>
        //public Dictionary<string, byte[]> Resources = new();
        public Dictionary<string, Resource> Resources = new();

        internal abstract Dictionary<string, string> TilesetXmls { get; }
        internal abstract Dictionary<string, byte[]> FileResources { get; }
        internal abstract Dictionary<string, ITexture2D> Samples { get; }
        internal abstract Dictionary<string, Color32[,,]> Grids { get; }
    }

    public abstract class ModelConfig<TTexture> : ModelConfig where TTexture : class
    {
        private Color32[,,] _initialGrid;
        private Dictionary<string, ITexture2D> _samples;
        private Dictionary<string, byte[]> _fileResources;
        private Dictionary<string, string> _tilesets;
        private Dictionary<string, Color32[,,]> _grids;

        /// <summary>
        /// Optional initial texture provided to generator. Must contain only default and custom defined colors. Default is null. Ignored if InitialGrid is set
        /// </summary>
        public TTexture InitialTexture = null;
        /// <summary>
        /// Optional initial grid provided to generator. Must contain only default and custom defined colors. Default is null
        /// </summary>
        public Color32[,,] InitialGrid = null;

        public void ResetCache()
        {
            _initialGrid = null;
            _samples = null;
            _fileResources = null;
            _tilesets = null;
            _grids = null;
        }

        protected abstract ITexture2D ConvertTexture(TTexture tex);


        internal override Color32[,,] InitialGridInternal => _initialGrid ??= InitialGrid ?? Helper.TextureToColor32Array(ConvertTexture(InitialTexture));
        internal override Dictionary<string, string> TilesetXmls => _tilesets ??= Resources.Where(x => x.Value is ResourceTilesetXml).ToDictionary(x => x.Key, x => (x.Value as ResourceTilesetXml)!.TilesetXml);
        internal override Dictionary<string, byte[]> FileResources => _fileResources ??= Resources.Where(x => x.Value is ResourceVoxBytes).ToDictionary(x => x.Key, x => (x.Value as ResourceVoxBytes)!.Vox);
        internal override Dictionary<string, ITexture2D> Samples => _samples ??= Resources.Where(x => x.Value is ResourceTexture).ToDictionary(x => x.Key, x => (x.Value as ResourceTexture)!.Texture);
        internal override Dictionary<string, Color32[,,]> Grids => _grids ??= Resources.Where(x => x.Value is ResourceColor32Grid).ToDictionary(x => x.Key, x => (x.Value as ResourceColor32Grid)!.ColorsGrid);
    }
}
