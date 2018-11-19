using Microsoft.Xna.Framework;
using System;

namespace Ark.Framework.GUI.Anchoring
{
    public interface IAnchorable
    {
        //AnchorComponent Anchor { get; }
        bool Anchored { get; }
        Vector2 Position { get; set; }
        Rectangle GetAnchorBounds();
        event EventHandler<AnchorMovedArgs> PositionChanged;
        event EventHandler<AnchorResizedArgs> Resized;
        void OnResized(AnchorResizedArgs args);
        void OnPositionChanged(AnchorMovedArgs args);

        void AnchorTo(IAnchorable target, AnchorAlignment alignment, PositionOffset offset);
        string Name { get; }
    }
}