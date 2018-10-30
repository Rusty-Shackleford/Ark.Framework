using Microsoft.Xna.Framework;
using System;

namespace Ark.Framework.GUI.Anchoring
{
    public interface IAnchorable
    {
        Vector2 DistanceMoved { get; set; }
        AnchorComponent Anchor { get; }
        Vector2 Position { get; }
        Vector2 OriginalPosition { get; }
        Rectangle Bounds { get; }
        Rectangle EffectiveBounds { get; }
        Vector2 Move(Vector2 distance);
        Vector2 MoveTo(Vector2 newPosition);
        event EventHandler<PositionChangedArgs> OnPositionChanged;
        event EventHandler OnDimmensionChanged;
        void AnchorTo(IAnchorable target, PositionType style, int offsetX = 0, int offsetY = 0, AnchorType anchorType = AnchorType.Bounds);
    }
}