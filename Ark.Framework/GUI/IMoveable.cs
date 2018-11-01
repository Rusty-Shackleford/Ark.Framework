using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;


namespace Ark.Framework.GUI
{
    internal interface IMoveable
    {
        void Drag(MouseEventArgs e);
        void DragStart(MouseEventArgs e);
        void DragEnd(MouseEventArgs e);

        Rectangle DragBounds { get; }
        //Rectangle DragBounds { get; }
        //bool Dragging { get; }
        //Vector2 OriginalPosition { get; }
        //void CancelDrag();
    }
}
