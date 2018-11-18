using Ark.Framework.GUI.Anchoring;
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
    public enum AnchorPreference
    {
        Panel,
        TopControl,
        BottomControl,
        RightControl,
        LeftControl
    }
    /// <summary>
    /// A panel is a special control that acts as a container for other controls.
    /// Visually, this is typically represented as an in-game window that contains 
    /// buttons, text, dropdowns and so on.  A panel is a self-sufficient container 
    /// that handles its own input handling and updating / drawing its children.
    /// </summary>
    public class Panel : Control, IMoveable, IUpdate
    {
        #region [ MakeClone ]
        public override Control MakeClone()
        {
            return new Panel(DefaultStyle, Position);
        }
        #endregion


        #region [ Members ]
        public ControlCollection Children { get; set; }
        internal InputHandler InputHandler { get; set; }
        #endregion


        #region [ Constructor ]
        public Panel(ControlStyle style, Vector2 position) : base(style)
        {
            Position = position;
            MovementEnabled = true;
            Children = new ControlCollection();
            InputHandler = new InputHandler(Children);
        }
        #endregion


        #region [ Children ]
        /// <summary>
        /// Adds a control to this panel without specific positioning
        /// instructions - you will need to place the control yourself.
        /// </summary>
        /// <param name="control">The control to add.</param>
        public Control Add(Control control)
        {
            Children.Add(control);
            return control;
        }

        /// <summary>
        /// Add a control and anchor it according to AnchorPreference
        /// </summary>
        /// <param name="control"><see cref="Control"/> to add.</param>
        /// <param name="pref">Method used to anchor the control to this panel.</param>
        /// <param name="offset">Offset from this panel.</param>
        public Control Add(Control control, AnchorPreference pref, PositionOffset offset)
        {
            switch (pref)
            {
                case AnchorPreference.Panel:
                    control.AnchorTo(this, AnchorAlignment.Inside_Top_Left, offset);
                    break;
                case AnchorPreference.TopControl:
                    var t = Children.FindTopControl();
                    if (t != null) control.AnchorTo(t, AnchorAlignment.Below_Center, offset);
                    break;
                case AnchorPreference.BottomControl:
                    var b = Children.FindBottomControl();
                    if (b != null) control.AnchorTo(b, AnchorAlignment.Below_Center, offset);
                    break;
                case AnchorPreference.RightControl:
                    var r = Children.FindBottomControl();
                    if (r != null) control.AnchorTo(r, AnchorAlignment.Below_Center, offset);
                    break;
                case AnchorPreference.LeftControl:
                    var l = Children.FindBottomControl();
                    if (l != null) control.AnchorTo(l, AnchorAlignment.Below_Center, offset);
                    break;
                default:
                    break;
            }
            throw new ArgumentException($"");
        }

        /// <summary>
        /// Add a control and anchor it to a child of this panel.
        /// </summary>
        /// <param name="control">Control to add.</param>
        /// <param name="anchor">Control to anchor to, must be a child.</param>
        /// <param name="offset">Offset from anchor.</param>
        public Control Add(Control control, AnchorSettings anchorSettings)
        {
            // Subtle note: By having the anchor already in this panel, we know
            // that it has already been properly placed and refreshed.
            if (Children.Contains(control))
            {
                control.AnchorTo(anchorSettings.Anchor, anchorSettings.Alignment, anchorSettings.Offset);
                return Add(control);
            }
            throw new ArgumentException($"Could not find anchor {anchorSettings.Anchor.Name} in this panel. " +
                $"Has it been added?");
        }
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


        #region [ Update ]
        public void Update(GameTime gameTime)
        {
            InputHandler.Update(gameTime);
        }
        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(CurrentStyle.Texture, Position, Color.White);
                for (int i = 0; i < Children.Count; i++)
                {
                    Children[i].Draw(spriteBatch);
                }
            }
        }
        #endregion
    }
}