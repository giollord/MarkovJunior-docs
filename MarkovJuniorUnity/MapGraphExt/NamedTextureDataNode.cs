using InsaneScatterbrain.MapGraph;
using InsaneScatterbrain.ScriptGraph;
using System;
using UnityEngine;

namespace MapGraphMarkovJunior
{
    [ScriptNode("Named texture data", "Markov Junior"), System.Serializable]
    public class NamedTextureDataNode : ProcessorNode
    {
        [InPort("Name", typeof(string), true), SerializeReference]
        private InPort nameIn;
        [InPort("Texture", typeof(Texture2D)), SerializeReference]
        private InPort textureIn;
        [InPort("Texture data", typeof(TextureData)), SerializeReference]
        private InPort textureDataIn;

        [OutPort("Named texture data", typeof(NamedTextureData)), SerializeReference]
        private OutPort resultOut = null;

        protected override void OnProcess()
        {
            if (!textureIn.IsConnected && !textureDataIn.IsConnected)
                throw new Exception("Nor Texture, nor TextureData are not connected");
            if (textureIn.IsConnected && textureDataIn.IsConnected)
                throw new Exception("At the same time can be connected either Texture, either TextureData, not both");

            var result = new NamedTextureData
            {
                Name = GetValue<string>(nameIn),
                Texture = GetValue<Texture2D>(textureIn),
                TextureData = GetValue<TextureData>(textureDataIn)
            };
            resultOut.Set(() => result);
        }

        private T GetValue<T>(InPort port, Func<T> defaultValueFactory = default) => port.IsConnected ? port.Get<T>() : (defaultValueFactory != null ? defaultValueFactory() : default(T));
    }
}
