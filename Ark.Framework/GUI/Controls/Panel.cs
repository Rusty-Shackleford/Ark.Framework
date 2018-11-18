using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark.Framework.GUI.Controls
{
    public class Panel : Control, IMoveable
    {
        #region [ MakeClone ]
        public override Control MakeClone()
        {
            return new Panel(DefaultStyle, Position);
        }
        #endregion


        #region [ Members ]
        #endregion


        #region [ Movement ]
        public Rectangle DragBounds
        {
            get { return CurrentStyle.DraggableOffset.ApplyToRectangle(Position, CurrentStyle.Size); }
        }

        public bool Moving { get; protected set; }
        public bool MovementEnabled { get; set; }
        public event EventHandler Moved;
        public event EventHandler MoveStarted;
        public event EventHandler MoveEnded;

        public void OnDrag(MouseEventArgs e)
        {
            if (Moving)
            {
                Position += e.DistanceMoved;
                Moved?.Invoke(this, e);
            }
        }

        public void OnDragStart(MouseEventArgs e)
        {
            if (Enabled && !Moving)
            {
                if (DragBounds.Contains(e.Position) && MovementEnabled)
                {
                    Moving = true;
                    Position += e.DistanceMoved;
                    MoveStarted?.Invoke(this, e);
                }
            }
        }

        public void OnDragEnd(MouseEventArgs e)
        {
            if (Moving)
            {
                Moving = false;
                Position += e.DistanceMoved;
                MoveEnded?.Invoke(this, e);
            }
        }
        #endregion



        #region [ Anchoring ]
        public override Rectangle GetAnchorBounds()
        {
            return CurrentStyle.AnchoringOffset.ApplyToRectangle(Position, CurrentStyle.Size);
        }
        #endregion


        #region [ Constructor ]
        public Panel(ControlStyle style, Vector2 position) : base(style)
        {
            Position = position;
            MovementEnabled = true;
        }
        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CurrentStyle.Texture, Position, Color.White);
        }
        #endregion
    }
}