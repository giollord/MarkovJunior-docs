using InsaneScatterbrain.ScriptGraph.Editor;
using InsaneScatterbrain.MapGraph.Editor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MapGraphMarkovJunior.Editor
{
    [ScriptNodeView(typeof(MarkovJuniorNode))]
    public class MarkovJuniorNodeView : ScriptNodeView
    {
        public MarkovJuniorNodeView(MarkovJuniorNode node, ScriptGraphView graphView) : base(node, graphView)
        {
            var label = new Label("Model XML:");
            label.style.fontSize = new StyleLength(16f);
            label.style.unityFontStyleAndWeight = FontStyle.Bold;
            mainContainer.Add(label);

            if (node != null)
            {
                var textField = new TextField(50000, true, false, '*');
                textField.value = node.xml;
                textField.RegisterValueChangedCallback(x =>
                {

                    node.xml = x.newValue;
                });
                mainContainer.Add(textField);
            }

            this.AddPreview<MarkovJuniorNode>(GetPreviewTexture);
        }

        private Texture2D GetPreviewTexture(MarkovJuniorNode node) => node.TextureData.ToTexture2D();
    }
}