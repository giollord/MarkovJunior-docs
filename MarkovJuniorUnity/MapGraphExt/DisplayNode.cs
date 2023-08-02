using InsaneScatterbrain.MapGraph;
using InsaneScatterbrain.ScriptGraph;
using UnityEngine;

namespace MapGraphMarkovJunior
{
    [ScriptNode("Display", "Markov Junior"), System.Serializable]
    public class DisplayNode : ProcessorNode
    {
        [InPort("In", typeof(TextureData), true), SerializeReference]
        private InPort inIn = null;

        [OutPort("Out", typeof(TextureData)), SerializeReference]
        private OutPort outOut = null;

        private TextureData textureData;

#if UNITY_EDITOR
        public TextureData TextureData => textureData;
#endif

        protected override void OnProcess()
        {
            textureData = inIn.Get<TextureData>();
            outOut.Set(() => textureData);
        }
    }
}