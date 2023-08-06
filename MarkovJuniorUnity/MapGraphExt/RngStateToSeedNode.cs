using InsaneScatterbrain.RandomNumberGeneration;
using InsaneScatterbrain.ScriptGraph;
using UnityEngine;
using Rng = InsaneScatterbrain.Services.Rng;

namespace MapGraphMarkovJunior
{
    [ScriptNode("RngState to seed", "Markov Junior"), System.Serializable]
    public class RngStateToSeedNode : ProcessorNode
    {
        [InPort("RNG State", typeof(RngState)), SerializeReference]
        private InPort rngStateIn = null;

        [OutPort("Seed", typeof(int)), SerializeReference]
        private OutPort seedOut = null;

        protected override void OnProcess()
        {
            var rng = Get<Rng>();
            if (rngStateIn.IsConnected)
            {
                rng.SetState(rngStateIn.Get<RngState>());
            }

            var seed = rng.Next();
            seedOut.Set(() => seed);
        }
    }
}
