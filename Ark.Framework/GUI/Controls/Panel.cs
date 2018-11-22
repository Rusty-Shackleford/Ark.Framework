using Ark.Framework.GUI.Anchoring;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using System;


namespace Ark.Framework.GUI.Controls
{
    public enum AnchorPreference
    {
        /// <summary>Anchor to Panel directly.</summary>
        Panel,
        /// <summary>Anchor to highest positioned control - highest Y axis value.</summary>
        Top,
        /// <summary>Anchor to lowest positioned control - lowest Y axis value.</summary>
        Bottom,
        /// <summary>Anchor to Control positioned furthest right - highest X axis value.</summary>
        Right,
        /// <summary>Anchor to Control positioned furthest left - lowest X axis value.</summary>
        Left,
        /// <summary>Anchor to the first child of Panel's control collection.</summary>
        First,
        /// <summary>Anchor to the last child of Panel's control collection.</summary>
        Last,
    }
    /// <summary>
    /// A panel is a special control that acts as a container for other controls.
    /// Visually, this is typically represented as an in-game window that contains 
    /// buttons, text, dropdowns and so on.  A panel is a self-sufficient container 
    /// that handles its own input handling and updating / drawing its children.
    /// </summary>
    public class Panel : Control, IMoveable, IUpdate
    {
        #region [ Members ]
        protected ControlCollection Children { get; set; }
        internal CollectionInputHandler _childrenInputHandler { get; set; }
        private readonly InputHandler _myInputHandler;
        public Viewport Viewport { get; private set; }

        public Rectangle ContentBounds
        {
            get { return PanelStyle().ViewportOffset.Apply(Position, CurrentStyle.Size); }
        }
        #endregion


        #region [ GetStyle ]
        // TODO: Investigate - hackjob?
        protected PanelControlStyle PanelStyle()
        {
            return (PanelControlStyle)CurrentStyle;
        }
        #endregion


        #region [ Constructor ]
        public Panel(PanelControlStyle style) : base(style)
        {
            MovementEnabled = true;
            Children = new ControlCollection();


            Viewport = new Viewport(ContentBounds);
            Viewport.AnchorTo(
                this, 
                AnchorAlignment.Inside_Top_Left, 
                PanelStyle().ViewportOffset.PositionOffset
                );

            _childrenInputHandler = new CollectionInputHandler(Children, Viewport);
            _myInputHandler = new InputHandler(this);
        }
        #endregion


        #region [ Children Management ]
        protected Control Add(Control control)
        {
            if (!Children.Contains(control))
                Children.Add(control);
            else
                throw new NotSupportedException($"This panel already contains this control. {control.ToString()}");
            return control;
        }

        /// <summary>
        /// Add a control and anchor it according to AnchorPreference.  If no suitable anchor can
        /// be found, the control is anchored to this panels inside-top-left.
        /// </summary>
        /// <param name="control"><see cref="Control"/> to add.</param>
        /// <param name="pref">Method used to anchor the control to this panel.</param>
        /// <param name="offset">Offset from this panel.</param>
        public Control Add(Control control, AnchorPreference pref, 
            PositionOffset offset, AnchorAlignment alignment = AnchorAlignment.Below_Center)
        {
            switch (pref)
            {
                case AnchorPreference.Panel:
                    control.AnchorTo(this, alignment, offset);
                    break;
                case AnchorPreference.Top:
                    var t = Children.FindTopControl();
                    if (t != null) control.AnchorTo(t, alignment, offset);
                    break;
                case AnchorPreference.Bottom:
                    var b = Children.FindBottomControl();
                    if (b != null) control.AnchorTo(b, alignment, offset);
                    break;
                case AnchorPreference.Right:
                    var r = Children.FindBottomControl();
                    if (r != null) control.AnchorTo(r, alignment, offset);
                    break;
                case AnchorPreference.Left:
                    var l = Children.FindBottomControl();
                    if (l != null) control.AnchorTo(l, alignment, offset);
                    break;
                case AnchorPreference.First:
                    control.AnchorTo(Children[0], alignment, offset);
                    break;
                case AnchorPreference.Last:
                    control.AnchorTo(Children[Children.Count -1 ], alignment, offset);
                    break;
                default:
                    break;
            }
            // Fallback: If no control found, anchor to this panel.
            if (control.Anchored == false)
            {
                // TODO: Log Message
                control.AnchorTo(this, AnchorAlignment.Inside_Top_Left, offset);
            }
            return Add(control);
        }


        public Control Add(Control control, IAnchorable anchor, AnchorAlignment align, PositionOffset offset)
        {
            control.AnchorTo(anchor, align, offset);
            return Add(control);
        }

        /// <summary>
        /// Add a control and anchor it to a child of this panel.
        /// </summary>
        /// <param name="control">Control to add.</param>
        /// <param name="anchor">Control to anchor to, must be a child.</param>
        /// <param name="offset">Offset from anchor.</param>
        //public Control Add(Control control, AnchorSettings anchorSettings)
        //{
        //    // Subtle note: By having the anchor already in this panel, we know
        //    // that it has already been properly placed and refreshed.
        //    if (Children.Contains(control))
        //    {
        //        control.AnchorTo(anchorSettings.Anchor, anchorSettings.Alignment, anchorSettings.Offset);
        //        return Add(control);
        //    }
        //    throw new ArgumentException($"Could not find anchor {anchorSettings.Anchor.Name} in this panel. " +
        //        $"Has it been added?");
        //}



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


        public Control FindControlByName(string name)
        {
            return Children.FindControlByName(name);
        }
        #endregion


        #region [ Movement ]
        public Rectangle DragBounds
        {
            get { return CurrentStyle.DraggableOffset.Apply(Position, CurrentStyle.Size); }
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
                spriteBatch.Draw(CurrentStyle.Texture, Position, Color.White);
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