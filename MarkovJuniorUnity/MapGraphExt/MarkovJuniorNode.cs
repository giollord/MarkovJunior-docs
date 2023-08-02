using InsaneScatterbrain.MapGraph;
using InsaneScatterbrain.ScriptGraph;
using MarkovJuniorLib;
using System.Linq;
using UnityEngine;

namespace MapGraphMarkovJunior
{
    [ScriptNode("Markov Junior", "Markov Junior"), System.Serializable]
    public class MarkovJuniorNode : ProcessorNode
    {
        [SerializeField]
        public string xml = @"<!-- Cave sample -->

<sequence values=""DA"">
  <prl in=""***/*D*/***"" out=""***/*A*/***""/>
  <prl in=""A"" out=""D"" p=""0.435"" steps=""1""/>
  <convolution neighborhood=""Moore"">
    <rule in=""A"" out=""D"" sum=""5..8"" values=""D""/>
    <rule in=""D"" out=""A"" sum=""6..8"" values=""A""/>
  </convolution>
  <all in=""AD/DA"" out=""AA/DA""/>
</sequence>
";

        [InPort("Size", typeof(Vector2Int), true), SerializeReference]
        private InPort sizeIn;

        [InPort("Steps", typeof(int)), SerializeReference]
        private InPort stepsIn;

        [InPort("Pixel size", typeof(int)), SerializeReference]
        private InPort pixelSizeIn;

        [InPort("Seed", typeof(int)), SerializeReference]
        private InPort seedIn;

        [InPort("Initial texture", typeof(TextureData)), SerializeReference]
        private InPort initialTextureIn;

        [InPort("Texture samples", typeof(NamedTextureData[])), SerializeReference]
        private InPort textureSamplesIn;

        [InPort("XML override", typeof(string)), SerializeReference]
        private InPort xmlOverrideIn;


        [OutPort("Result", typeof(TextureData)), SerializeReference]
        private OutPort resultOut = null;

        [OutPort("Result 2", typeof(TextureData)), SerializeReference]
        private OutPort result2Out = null;

        [OutPort("Result 3", typeof(TextureData)), SerializeReference]
        private OutPort result3Out = null;

        [OutPort("Result 4", typeof(TextureData)), SerializeReference]
        private OutPort result4Out = null;


        private TextureData textureData;

#if UNITY_EDITOR
        public TextureData TextureData => textureData;
#endif

        protected override void OnProcess()
        {
            var size = GetValue<Vector2Int>(sizeIn);
            var initialTextureData = GetValue<TextureData>(initialTextureIn);
            var initialTexture = initialTextureData == null ? null : initialTextureData.ToTexture2D();
            var pixelSize = GetValue(pixelSizeIn, () => 1);
            var steps = GetValue(stepsIn, () => 10000);
            var textureSamples = GetValue<NamedTextureData[]>(textureSamplesIn, () => new NamedTextureData[0])
                .ToDictionary(x => x.Name, x => x.Texture != null ? x.Texture : x.TextureData.ToTexture2D());

            var resultNodes = new[] { result2Out, result3Out, result4Out }
                .Where(x => x.IsConnected)
                .Prepend(resultOut).ToList();

            var seed = GetValue(seedIn, () => Random.Range(0, int.MaxValue));
            var rnd = new System.Random(seed);
            var seeds = seedIn.IsConnected ?
                Enumerable.Range(0, resultNodes.Count).Select(i => i == 0 ? seed : rnd.Next()).ToArray() :
                Enumerable.Range(0, resultNodes.Count).Select(_ => Random.Range(0, int.MaxValue)).ToArray();
            var modelXml = GetValue(xmlOverrideIn, () => xml);

            var cfg = new ModelConfig
            {
                Amount = resultNodes.Count,
                ModelXML = modelXml,
                InitialTexture = initialTexture,
                PixelSize = pixelSize,
                Steps = steps,
                Seeds = seeds,
                Width_MX = size.x,
                Height_MY = size.y,
                Samples = textureSamples
            };

            var result = MarkovJuniorRunner.Run(cfg).ToList();

            for (var i = 0; i < result.Count; i++)
            {
                var tex = result[i].Texture;
                var instanceProvider = Get<IInstanceProvider>();
                var texData = instanceProvider.Get<TextureData>();
                //var texData = new TextureData();
                TextureData.CreateFromTexture(texData, tex);
                resultNodes[i].Set(() => texData);

                if (i == 0)
                    textureData = texData;
            }
        }

        private T GetValue<T>(InPort port, System.Func<T> defaultValueFactory = default) => port.IsConnected ? port.Get<T>() : (defaultValueFactory != null ? defaultValueFactory() : default(T));
    }
}