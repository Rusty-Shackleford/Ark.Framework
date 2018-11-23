using Ark.Framework.GUI.Controls;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;


namespace Ark.Framework.GUI
{
    internal class PanelInputHandler : IUpdate
    {
        #region [ Members ]
        Panel _owner;
        private MouseListener mouse = new MouseListener(new MouseListenerSettings());
        //private KeyboardListener keyboard = new KeyboardListener(new KeyboardListenerSettings());

        private IMoveable _movingItem;
        private Control _hoveredItem;
        private Control _pressedItem;
        #endregion


        #region [ Constructor ]
        public PanelInputHandler(Panel owner)
        {
            _owner = owner;

            mouse.MouseMoved += Hover;
            mouse.MouseDown += OnMouseDown;
            mouse.MouseUp += OnMouseUp;

            mouse.MouseDragStart += MoveStart;
            mouse.MouseDrag += Move;
            mouse.MouseDragEnd += MoveEnd;
        }
        #endregion


        #region [ Hover ]
        protected virtual void Hover(object sender, MouseEventArgs e)
        {
            if (_hoveredItem != null)
            {
                if (!_hoveredItem.HoverBounds.Contains(e.Position))
                {
                    _hoveredItem.OnMouseLeft(e);
                    _hoveredItem = null;
                }
            }

            if (_owner.Viewport.Bounds.Contains(e.Position))
            {
                Control c = _owner.Children.GetItemAtPoint(e.Position);

                if (c != null)
                {
                    _hoveredItem = c;
                    _hoveredItem.OnMouseEntered(e);
                }
            }

        }
        #endregion


        #region [ Movement ]
        protected virtual void MoveStart(object sender, MouseEventArgs e)
        {
            if (_owner.Viewport.Bounds.Contains(e.Position))
            {
                Control c = _owner.Children.GetItemAtPoint(e.Position);
                if (_movingItem == null && c != null)
                {
                    if (c is IMoveable)
                    {
                        IMoveable movingItem = (IMoveable)c;
                        if (movingItem.DragBounds.Contains(e.Position))
                        {
                            _movingItem = (IMoveable)c;
                            _movingItem.OnDragStart(e);
                        }
                    }
                }
            }

        }


        protected virtual void Move(object sender, MouseEventArgs e)
        {
            //TODO: May need to implement Viewport check
            if (_movingItem != null)
            {
                _movingItem.OnDrag(e);
            }
        }


        protected virtual void MoveEnd(object sender, MouseEventArgs e)
        {
            //TODO: May need to implement Viewport check
            if (_movingItem != null)
            {
                _movingItem.OnDragEnd(e);
                _movingItem = null;
            }
        }
        #endregion


        #region [ Mouse Up/Down ]
        protected virtual void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (_owner.Viewport.Bounds.Contains(e.Position))
            {
                Control c = _owner.Children.GetItemAtPoint(e.Position);
                if (c != null)
                {
                    _pressedItem = c;
                    _pressedItem.OnMouseDown(e);
                }
            }

        }

        protected virtual void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (_pressedItem != null)
            {
                _pressedItem.OnMouseUp(e);
                _pressedItem = null;
            }
        }
        #endregion


        #region [ Update ]
        public void Update(GameTime gameTime)
        {
            mouse.Update(gameTime);
        }
        #endregion
    }
}
