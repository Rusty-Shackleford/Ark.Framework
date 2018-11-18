using Ark.Framework.GUI.Controls;

namespace Ark.Framework.GUI.Anchoring
{
    public struct AnchorSettings
    {
        public Control Anchor;
        public AnchorAlignment Alignment;
        public PositionOffset Offset;

        public AnchorSettings(Control anchor, AnchorAlignment alignment, PositionOffset offset)
        {
            Anchor = anchor;
            Alignment = alignment;
            Offset = offset;
        }

    }
}
