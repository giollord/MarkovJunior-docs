using InsaneScatterbrain.ScriptGraph;
using MarkovJuniorLib;
using UnityEngine;

namespace MapGraphMarkovJunior
{
    [ScriptNode("Markov Junior Colors", "Markov Junior"), System.Serializable]
    public class MarkovJuniorColorsNode : ProcessorNode
    {
        [OutPort("Black, B", typeof(Color32)), SerializeReference] private OutPort black = null;
        [OutPort("Indigo, I", typeof(Color32)), SerializeReference] private OutPort indigo = null;
        [OutPort("Purple, P", typeof(Color32)), SerializeReference] private OutPort purple = null;
        [OutPort("Emerald, E", typeof(Color32)), SerializeReference] private OutPort emerald = null;
        [OutPort("browN, N", typeof(Color32)), SerializeReference] private OutPort brown = null;
        [OutPort("Dead, Dark, D", typeof(Color32)), SerializeReference] private OutPort deadDark = null;
        [OutPort("Alive, grAy, A", typeof(Color32)), SerializeReference] private OutPort aliveGray = null;
        [OutPort("White, W", typeof(Color32)), SerializeReference] private OutPort white = null;
        [OutPort("Red, R", typeof(Color32)), SerializeReference] private OutPort red = null;
        [OutPort("Orange, O", typeof(Color32)), SerializeReference] private OutPort orange = null;
        [OutPort("Yellow, Y", typeof(Color32)), SerializeReference] private OutPort yellow = null;
        [OutPort("Green, G", typeof(Color32)), SerializeReference] private OutPort green = null;
        [OutPort("blUe, U", typeof(Color32)), SerializeReference] private OutPort blue = null;
        [OutPort("Slate, S", typeof(Color32)), SerializeReference] private OutPort slate = null;
        [OutPort("pinK, K", typeof(Color32)), SerializeReference] private OutPort pink = null;
        [OutPort("Fawn, F", typeof(Color32)), SerializeReference] private OutPort fawn = null;
        [OutPort("Cyan, C", typeof(Color32)), SerializeReference] private OutPort cyan = null;
        [OutPort("Honey, H", typeof(Color32)), SerializeReference] private OutPort honey = null;
        [OutPort("Jungle, J", typeof(Color32)), SerializeReference] private OutPort jungle = null;
        [OutPort("Light, L", typeof(Color32)), SerializeReference] private OutPort light = null;
        [OutPort("Magenta, M", typeof(Color32)), SerializeReference] private OutPort magenta = null;
        [OutPort("aQua, Q", typeof(Color32)), SerializeReference] private OutPort aqua = null;
        [OutPort("Teal, T", typeof(Color32)), SerializeReference] private OutPort teal = null;
        [OutPort("oliVe, V", typeof(Color32)), SerializeReference] private OutPort olive = null;

        protected override void OnProcess()
        {
            var pallette = MarkovJuniorRunner.GetDefaultPallette();

            black.Set(() => pallette['B']);
            indigo.Set(() => pallette['I']);
            purple.Set(() => pallette['P']);
            emerald.Set(() => pallette['E']);
            brown.Set(() => pallette['N']);
            deadDark.Set(() => pallette['D']);
            aliveGray.Set(() => pallette['A']);
            white.Set(() => pallette['W']);
            red.Set(() => pallette['R']);
            orange.Set(() => pallette['O']);
            yellow.Set(() => pallette['Y']);
            green.Set(() => pallette['G']);
            blue.Set(() => pallette['U']);
            slate.Set(() => pallette['S']);
            pink.Set(() => pallette['K']);
            fawn.Set(() => pallette['F']);
            cyan.Set(() => pallette['C']);
            honey.Set(() => pallette['H']);
            jungle.Set(() => pallette['J']);
            light.Set(() => pallette['L']);
            magenta.Set(() => pallette['M']);
            aqua.Set(() => pallette['Q']);
            teal.Set(() => pallette['T']);
            olive.Set(() => pallette['V']);
        }
    }
}