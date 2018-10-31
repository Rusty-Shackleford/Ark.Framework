using Ark.Framework.GUI.Anchoring;
using Ark.Framework.GUI.Controls.Styles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using System;


namespace Ark.Framework.GUI.Controls
{
    //TODO: LEFT OFF HERE
    // 10/30/2018: Sorting out the import of these Monogame.Forms gui elements, starting with control
    // I've made major changed to "ControlStyle" in order to simplify its job - it should now only hold
    // a Texture and the relevant data for its various bounding rectangles (hoverbounds, etc)
    // this cascades down to anchoring, which appears to be in the middle of major changes from my PREVIOUS
    // attempt at this.  Do what is necessary to make it work, streamline anchoring, and remove and "rendering"
    // references and just have the controls draw themselves.
    // Next: I probably need to overhaul the event system for controls to simplify these, and place more (or all)
    // of the responsibility for these on the sub-classes that will use them, as you will never have a "control"
    // anyway, and so you can have different controls with different events.  An interface can be used to group
    // similar functionality for controls (i.e. if two controls can both be clicked, have an IClickable interface)
    public abstract class Control : IAnchorable
    {
        protected Control(TextureControlStyle style)
        {
            Visible = true;
            Enabled = true;
            DefaultStyle = style;
            ActiveStyle = DefaultStyle;
        }


        #region [ Anchoring ]
        public Rectangle AnchorBounds
        {
            get
            {
                return ActiveStyle.AnchoringOffset.ApplyToRectangle(Position, ActiveStyle.Size);
            }
        }

        public event EventHandler<AnchorMovedArgs> OnPositionChanged;
        public AnchorComponent Anchor { get; private set; }

        public void AnchorTo(IAnchorable target, AnchorAlignment style, int offsetX = 0, int offsetY = 0, AnchorType anchorType = AnchorType.Bounds)
        {
            if (target.GetHashCode() != GetHashCode())
            {
                RemoveAnchor();
                Anchor = new AnchorComponent(target, this, anchorType, style, new PositionOffset(offsetX, offsetY));
                return;
            }
            Console.WriteLine("WARNING: Attempted to anchor this object to itself.");
        }

        public void AnchorTo(AnchorToArgs args)
        {
            AnchorTo(args.AnchorTo, args.PositionType, args.OffsetX, args.OffsetY, args.AnchorType);
        }

        public void RemoveAnchor()
        {
            if (Anchor != null)
            {
                Anchor.RemoveAnchor();
                Anchor = null;
            }
        }
        #endregion


        #region [ Dimmensional ]
        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            private set
            {
                if (value != _position)
                {
                    var distanceMoved = _position - value;
                    OnPositionChanged?.Invoke(this, new AnchorMovedArgs(distanceMoved));
                    _position = value;

                }
            }
        }

        public virtual int Height
        {
            get { return ActiveStyle.Size.Height; }
        }

        public virtual int Width
        {
            get { return ActiveStyle.Size.Width; }
        }

        public virtual Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    Width,
                    Height
                );
            }
        }

        public virtual Rectangle HoverBounds
        {
            get
            {
                return ActiveStyle.HoverOffset.ApplyToRectangle(Position, ActiveStyle.Size);
            }
        }

        public virtual Rectangle DraggableBounds
        {
            get
            {
                return ActiveStyle.DraggableOffset.ApplyToRectangle(Position, ActiveStyle.Size);
            }
        }

        public virtual Rectangle InteractiveBounds
        {
            get
            {
                return ActiveStyle.InteractiveOffset.ApplyToRectangle(Position, ActiveStyle.Size);
            }
        }
        #endregion


        #region [ Style ]
        private TextureControlStyle _defaultStyle;
        public virtual TextureControlStyle DefaultStyle
        {
            get { return _defaultStyle; }
            set
            {
                if (value.Size != _defaultStyle.Size)
                {
                    _defaultStyle = value;
                    OnDimmensionChanged?.Invoke(this, EventArgs.Empty);
                    OnPropertyChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private TextureControlStyle _activeStyle;
        public TextureControlStyle ActiveStyle
        {
            get { return _activeStyle; }
            set
            {
                if (value != _activeStyle)
                {
                    //TODO: Is there really a use case in which the anchor dimmensions 
                    // of the control change based on the curent style of it?
                    if (!_activeStyle.EqualDimmensionsTo(value))
                        OnDimmensionChanged?.Invoke(this, EventArgs.Empty);
                    _activeStyle = value;
                }
            }
        }
        #endregion


        #region [ Events ]
        public event EventHandler OnGainedFocus;
        public event EventHandler OnLostFocus;
        public event EventHandler OnClicked;
        public event EventHandler OnMouseOver;
        public event EventHandler OnMouseOut;
        public event EventHandler OnPropertyChanged;
        public event EventHandler OnDimmensionChanged;
        #endregion


        #region [ State ]
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public bool Pressed { get; protected set; }
        public bool Hovered { get; protected set; }
        public bool Dragging { get; protected set; }

        private bool _hasFocus;
        public bool HasFocus
        {
            get { return _hasFocus; }
            set
            {
                if (value != _hasFocus)
                {
                    _hasFocus = value;
                    if (_hasFocus)
                    {
                        OnGainedFocus?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        OnLostFocus?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public ControlState State
        {
            get
            {
                if (!Enabled)
                {
                    return ControlState.Disabled;
                }
                if (Hovered)
                {
                    if (Pressed)
                    {
                        return ControlState.Pressed;
                    }
                    return ControlState.Hovered;
                }
                if (HasFocus)
                {
                    return ControlState.Activated;
                }
                return ControlState.Default;
            }
        }
        #endregion


        #region [ Mouse Actions]
        public virtual void MouseOver(MouseEventArgs e)
        {
            if (Enabled && !Hovered)
            {
                if (HoverBounds.Contains(e.Position))
                {
                    Hovered = true;
                    OnMouseOver?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public virtual void MouseOut(MouseEventArgs e)
        {
            if (Enabled && Hovered)
            {
                if (!HoverBounds.Contains(e.Position))
                {
                    Hovered = false;
                    OnMouseOut?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public virtual void Press(MouseEventArgs e)
        {
            if (Enabled)
            {
                Pressed = true;
                HasFocus = true;
            }
        }
        public virtual void Click(MouseEventArgs e)
        {
            if (Enabled && Hovered)
            {
                Pressed = false;
                OnClicked?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                HasFocus = false;
            }
        }
        #endregion


        #region [ Virtual - Update / Draw ]
        public virtual void Update(GameTime gameTime) { }
        public abstract void Draw(SpriteBatch spriteBatch);
        #endregion
    }
}
