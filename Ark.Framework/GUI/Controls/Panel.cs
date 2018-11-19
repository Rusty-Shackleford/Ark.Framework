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
            return new Panel((PanelControlStyle)DefaultStyle, Position);
        }
        #endregion

        //TODO: LEFT OFF HERE
        // Implement Panel Input Handling and Test.

        #region [ Members ]
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if(value != _title)
                {
                    _title = value;
                    Label = new Label(GetCurrentStyle(), _title);
                    Label.AnchorTo(this, AnchorAlignment.Inside_Top_Center, new PositionOffset(0, 5));
                }

            }
        }
        private Label Label { get; set; }
        protected ControlCollection Children { get; set; }
        internal CollectionInputHandler _childrenInputHandler { get; set; }
        private readonly InputHandler _myInputHandler;
        protected override ControlStyle CurrentStyle()
        {
            return GetCurrentStyle();
        }
        public new PanelControlStyle GetCurrentStyle()
        {
            return (PanelControlStyle)currentStyle;
        }
        public Viewport Viewport { get; private set; }
        #endregion


        #region [ Constructor ]
        public Panel(PanelControlStyle style, Vector2 position) : base(style)
        {
            Position = position;
            MovementEnabled = true;
            Children = new ControlCollection();

            Rectangle view = GetCurrentStyle().ViewportOffset.Apply(Position, GetCurrentStyle().Size);

            Viewport = new Viewport(view);
            Viewport.AnchorTo(this, AnchorAlignment.Inside_Top_Left, new PositionOffset(4, 24));
            _childrenInputHandler = new CollectionInputHandler(Children, Viewport);
            _myInputHandler = new InputHandler(this);
        }
        #endregion


        #region [ Children Management ]
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
        /// Add a control and anchor it according to AnchorPreference.  If no suitable anchor can
        /// be found, the control is anchored to this panels inside-top-left.
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
            // Fallback: If no control found, anchor to this panel.
            if (control.Anchored == false)
            {
                control.AnchorTo(this, AnchorAlignment.Inside_Top_Left, offset);
            }
            return Add(control);
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

        /// <summary>
        /// Remove an existing control from this panel.
        /// </summary>
        /// <param name="control">Reference to control that will be removed.</param>
        /// <returns></returns>
        public Control Remove(Control control)
        {
            Children.Remove(control);
            return control;
        }
        #endregion


        #region [ Movement ]
        public Rectangle DragBounds
        {
            get { return GetCurrentStyle().DraggableOffset.Apply(Position, GetCurrentStyle().Size); }
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
            return GetCurrentStyle().AnchoringOffset.Apply(Position, GetCurrentStyle().Size);
        }
        #endregion


        #region [ Update ]
        public void Update(GameTime gameTime)
        {
            _myInputHandler.Update(gameTime);
            _childrenInputHandler.Update(gameTime);
        }
        #endregion


        #region [ Draw ]
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(GetCurrentStyle().Texture, Position, Color.White);
                Label?.Draw(spriteBatch);
                for (int i = 0; i < Children.Count; i++)
                {
                    Children[i].Draw(spriteBatch);
                }
            }
        }
        #endregion
    }
}