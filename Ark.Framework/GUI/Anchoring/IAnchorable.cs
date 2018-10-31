using Microsoft.Xna.Framework;
using System;

namespace Ark.Framework.GUI.Anchoring
{
    public interface IAnchorable
    {
        AnchorComponent Anchor { get; }
        Vector2 Position { get; }
        Rectangle AnchorBounds { get; }
        //Vector2 Move(Vector2 distance);
        //Vector2 MoveTo(Vector2 newPosition);
        event EventHandler<AnchorMovedArgs> OnPositionChanged;
        event EventHandler OnDimmensionChanged;
        void AnchorTo(IAnchorable target, PositionType style, int offsetX = 0, int offsetY = 0, AnchorType anchorType = AnchorType.Bounds);
    }
}