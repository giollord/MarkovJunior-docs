using InsaneScatterbrain.ScriptGraph.Editor;
using InsaneScatterbrain.MapGraph.Editor;
using UnityEngine;

namespace MapGraphMarkovJunior.Editor
{
    [ScriptNodeView(typeof(DisplayNode))]
    public class DisplayNodeView : ScriptNodeView
    {
        public DisplayNodeView(DisplayNode node, ScriptGraphView graphView) : base(node, graphView)
        {
            this.AddPreview<DisplayNode>(GetPreviewTexture);
        }

        private Texture2D GetPreviewTexture(DisplayNode node) => node.TextureData.ToTexture2D();
    }
}
