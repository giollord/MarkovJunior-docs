using InsaneScatterbrain.MapGraph;
using InsaneScatterbrain.ScriptGraph;
using System.Linq;
using UnityEngine;

namespace MapGraphMarkovJunior
{
    [ScriptNode("Append NamedTextureData[]", "Markov Junior"), System.Serializable]
    public class NamedTextureDataArrayAppendNode : ProcessorNode
    {
        [InPort("Array", typeof(NamedTextureData[])), SerializeReference]
        private InPort arrayIn;

        [InPort("1. Append one", typeof(NamedTextureData)), SerializeReference]
        private InPort append1In;

        [InPort("2. Append array", typeof(NamedTextureData[])), SerializeReference]
        private InPort appendArr1In;

        [InPort("3. Append one", typeof(NamedTextureData)), SerializeReference]
        private InPort append2In;

        [InPort("4. Append array", typeof(NamedTextureData[])), SerializeReference]
        private InPort appendArr2In;

        [OutPort("Result", typeof(NamedTextureData[])), SerializeReference]
        private OutPort resultOut = null;

        protected override void OnProcess()
        {
            var array = GetValue(arrayIn, () => new NamedTextureData[0]);
            var append1 = GetValue<NamedTextureData>(append1In);
            var append1Arr = GetValue<NamedTextureData[]>(appendArr1In, () => new NamedTextureData[0]);
            var append2 = GetValue<NamedTextureData>(append2In);
            var append2Arr = GetValue<NamedTextureData[]>(appendArr2In, () => new NamedTextureData[0]);

            var result = array.Append(append1).Concat(append1Arr).Append(append2).Concat(append2Arr)
                .Where(x => x != null).ToArray();
            resultOut.Set(() => result);
        }

        private T GetValue<T>(InPort port, System.Func<T> defaultValueFactory = default) => port.IsConnected ? port.Get<T>() : (defaultValueFactory != null ? defaultValueFactory() : default(T));
    }
}