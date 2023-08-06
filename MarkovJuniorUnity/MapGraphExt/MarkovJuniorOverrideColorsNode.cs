using InsaneScatterbrain.ScriptGraph;
using MarkovJuniorLib;
using System.Collections.Generic;
using UnityEngine;

namespace MapGraphMarkovJunior
{
    [ScriptNode("Override Colors", "Markov Junior"), System.Serializable]
    public class MarkovJuniorOverrideColorsNode : ProcessorNode
    {
        [InPort("Black, B", typeof(Color32)), SerializeReference] private InPort black = null;
        [InPort("Indigo, I", typeof(Color32)), SerializeReference] private InPort indigo = null;
        [InPort("Purple, P", typeof(Color32)), SerializeReference] private InPort purple = null;
        [InPort("Emerald, E", typeof(Color32)), SerializeReference] private InPort emerald = null;
        [InPort("browN, N", typeof(Color32)), SerializeReference] private InPort brown = null;
        [InPort("Dead, Dark, D", typeof(Color32)), SerializeReference] private InPort deadDark = null;
        [InPort("Alive, grAy, A", typeof(Color32)), SerializeReference] private InPort aliveGray = null;
        [InPort("White, W", typeof(Color32)), SerializeReference] private InPort white = null;
        [InPort("Red, R", typeof(Color32)), SerializeReference] private InPort red = null;
        [InPort("Orange, O", typeof(Color32)), SerializeReference] private InPort orange = null;
        [InPort("Yellow, Y", typeof(Color32)), SerializeReference] private InPort yellow = null;
        [InPort("Green, G", typeof(Color32)), SerializeReference] private InPort green = null;
        [InPort("blUe, U", typeof(Color32)), SerializeReference] private InPort blue = null;
        [InPort("Slate, S", typeof(Color32)), SerializeReference] private InPort slate = null;
        [InPort("pinK, K", typeof(Color32)), SerializeReference] private InPort pink = null;
        [InPort("Fawn, F", typeof(Color32)), SerializeReference] private InPort fawn = null;
        [InPort("Cyan, C", typeof(Color32)), SerializeReference] private InPort cyan = null;
        [InPort("Honey, H", typeof(Color32)), SerializeReference] private InPort honey = null;
        [InPort("Jungle, J", typeof(Color32)), SerializeReference] private InPort jungle = null;
        [InPort("Light, L", typeof(Color32)), SerializeReference] private InPort light = null;
        [InPort("Magenta, M", typeof(Color32)), SerializeReference] private InPort magenta = null;
        [InPort("aQua, Q", typeof(Color32)), SerializeReference] private InPort aqua = null;
        [InPort("Teal, T", typeof(Color32)), SerializeReference] private InPort teal = null;
        [InPort("oliVe, V", typeof(Color32)), SerializeReference] private InPort olive = null;

        [OutPort("Colors", typeof(ModelColor[])), SerializeReference] private OutPort colorsOut = null;

        protected override void OnProcess()
        {
            var overrides = new List<ModelColor>();

            AddColor(black, 'B', overrides);
            AddColor(indigo, 'I', overrides);
            AddColor(purple, 'P', overrides);
            AddColor(emerald, 'E', overrides);
            AddColor(brown, 'N', overrides);
            AddColor(deadDark, 'D', overrides);
            AddColor(aliveGray, 'A', overrides);
            AddColor(white, 'W', overrides);
            AddColor(red, 'R', overrides);
            AddColor(orange, 'O', overrides);
            AddColor(yellow, 'Y', overrides);
            AddColor(green, 'G', overrides);
            AddColor(blue, 'U', overrides);
            AddColor(slate, 'S', overrides);
            AddColor(pink, 'K', overrides);
            AddColor(fawn, 'F', overrides);
            AddColor(cyan, 'C', overrides);
            AddColor(honey, 'H', overrides);
            AddColor(jungle, 'J', overrides);
            AddColor(light, 'L', overrides);
            AddColor(magenta, 'M', overrides);
            AddColor(aqua, 'Q', overrides);
            AddColor(teal, 'T', overrides);
            AddColor(olive, 'V', overrides);

            var overridesArr = overrides.ToArray();
            colorsOut.Set(() => overridesArr);
        }

        private void AddColor(InPort port, char colorSymbol, List<ModelColor> list)
        {
            if (!port.IsConnected)
                return;
            list.Add(new ModelColor
            {
                Color = port.Get<Color32>(),
                Symbol = colorSymbol
            });
        }
    }
}
