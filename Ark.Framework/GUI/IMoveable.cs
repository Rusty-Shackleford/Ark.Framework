using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;


namespace Ark.Framework.GUI
{
    internal interface IMoveable
    {
        void OnDrag(MouseEventArgs e);
        void OnDragStart(MouseEventArgs e);
        void OnDragEnd(MouseEventArgs e);

        Rectangle DragBounds { get; }
        //Rectangle DragBounds { get; }
        //bool Dragging { get; }
        //Vector2 OriginalPosition { get; }
        //void CancelDrag();
    }
}
