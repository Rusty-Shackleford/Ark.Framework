using Microsoft.Xna.Framework;
using System;

namespace Ark.Framework.GUI.Anchoring
{
    public interface IAnchorable
    {
        AnchorComponent Anchor { get; }
        Vector2 Position { get; set; }
        Rectangle GetAnchorBounds();
        event EventHandler<AnchorMovedArgs> OnPositionChanged;
        event EventHandler DimmensionChanged;
        void AnchorTo(IAnchorable target, AnchorAlignment alignment, PositionOffset offset);
        string Name { get; }
    }
}