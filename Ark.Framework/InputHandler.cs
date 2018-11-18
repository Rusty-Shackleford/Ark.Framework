using Ark.Framework.GUI;
using Ark.Framework.GUI.Controls;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Input.InputListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark.Framework
{
    internal class InputHandler
    {
        #region [ Members ]
        private Control _control;
        private MouseListener mouse = new MouseListener(new MouseListenerSettings());
        private bool _moving;
        #endregion


        #region [ Constructor ]
        public InputHandler(Control control)
        {
            _control = control;
            mouse.MouseMoved += Hover;
            mouse.MouseDown += OnMouseDown;
            mouse.MouseUp += OnMouseUp;

            mouse.MouseDragStart += MoveStart;
            mouse.MouseDrag += Move;
            mouse.MouseDragEnd += MoveEnd;
        }
        #endregion


        #region [ Hover ]
        private void Hover(object sender, MouseEventArgs e)
        {
            if (_control.Hovered && !_control.HoverBounds.Contains(e.Position))
            {
                _control.OnMouseLeft(e);
                return;
            }
            else
            {
                _control.OnMouseEntered(e);
            }
            
        }
        #endregion

        #region [ Movement ]
        private void MoveStart(object sender, MouseEventArgs e)
        {
            if (_control is IMoveable)
            {
                IMoveable c = (IMoveable)_control;
                if (!c.Moving && c.DragBounds.Contains(e.Position))
                {
                    c.OnDragStart(e);
                    _moving = true;
                }
            }
        }

        private void Move(object sender, MouseEventArgs e)
        {
            if (_moving)
            {
                IMoveable c = (IMoveable)_control;
                c.OnDrag(e);
            }
        }

        private void MoveEnd(object sender, MouseEventArgs e)
        {
            if (_moving)
            {
                IMoveable c = (IMoveable)_control;
                c.OnDragEnd(e);
                _moving = false;
            }
        }
        #endregion

        #region [ Mouse Up/Down ]
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            _control.OnMouseDown(e);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            _control.OnMouseDown(e);
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
