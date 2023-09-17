using MarkovJuniorLib.ToOverride;
using System.Collections.Generic;
using System.Linq;

namespace MarkovJuniorLib
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
        /// Optional initial texture provided to generator. Must contain only default and custom defined colors. Default is null
        /// </summary>
        internal abstract ITexture2D InitialTextureInternal { get; }
        /// <summary>
        /// Samples to use with "sample", "fin", "fout" and "file" attributes. Key is sample name, value is texture that must contain only default and custom defined colors. Default is empty dictionary
        /// </summary>
        internal abstract Dictionary<string, ITexture2D> SamplesInternal { get; }
        /// <summary>
        /// Tileset and tiles XML configurations. Key is tileset name, value is tileset configuration XML. Key is tileset name or tile configuration in format "TILESETNAME/TILENAME", value is tileset or tile XML. Default is empty dictionary
        /// </summary>
        public Dictionary<string, string> TilesetXmls = new();
        /// <summary>
        /// Contents of .vox files to use with "sample", "fin", "fout" and "file" attributes. Key is resource name, value is .vox file. Default is empty dictionary
        /// </summary>
        public Dictionary<string, byte[]> Resources = new();
    }

    public abstract class ModelConfig<TTexture> : ModelConfig where TTexture : class
    {
        private ITexture2D _initialTexture;
        private Dictionary<string, ITexture2D> _samples;

        /// <summary>
        /// Optional initial texture provided to generator. Must contain only default and custom defined colors. Default is null
        /// </summary>
        public TTexture InitialTexture = null;
        /// <summary>
        /// Samples to use with "sample", "fin", "fout" and "file" attributes. Key is sample name, value is texture that must contain only default and custom defined colors. Default is empty dictionary
        /// </summary>
        public Dictionary<string, TTexture> Samples = new();

        public void ResetCache()
        {
            _initialTexture = null;
            _samples = null;
        }

        protected abstract ITexture2D ConvertTexture(TTexture tex);

        internal override ITexture2D InitialTextureInternal => _initialTexture ??= ConvertTexture(InitialTexture);
        internal override Dictionary<string, ITexture2D> SamplesInternal => _samples ??= Samples.ToDictionary(x => x.Key, x => ConvertTexture(x.Value));
    }
}
