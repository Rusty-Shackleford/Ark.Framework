using Ark.Framework.GUI.Anchoring;
using Ark.Framework.GUI.Controls.Styles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using System;


namespace Ark.Framework.GUI.Controls
{
    public abstract class Control : IAnchorable
    {
        /// <summary>
        /// Make a new instance of this control with its same settings.
        /// </summary>
        /// <returns></returns>
        public abstract Control MakeClone();


        /// <summary>
        /// Create a new control.
        /// </summary>
        /// <param name="style">The default style to be used to render the control.</param>
        #region [ Constructor ]
        protected Control(TextureControlStyle style)
        {
            Visible = true;
            Enabled = true;
            DefaultStyle = style;
            _currentStyle = DefaultStyle;
            HoveredStyle = DefaultStyle;
            PressedStyle = DefaultStyle;
        }
        #endregion



        #region [ Anchoring ]
        public Rectangle AnchorBounds
        {
            get
            {
                return CurrentStyle.AnchoringOffset.ApplyToRectangle(Position, CurrentStyle.Size);
            }
        }

        public event EventHandler<AnchorMovedArgs> OnPositionChanged;
        public AnchorComponent Anchor { get; private set; }

        public void AnchorTo(IAnchorable target, AnchorAlignment alignment, PositionOffset offset)
        {
            // never anchor to yourself, it's dangerous
            if (target.GetHashCode() != GetHashCode())
            {
                RemoveAnchor();
                Anchor = new AnchorComponent(target, this, alignment, offset);
                Position = Anchor.AnchoredPosition;
                return;
            }
            Console.WriteLine("WARNING: Attempted to anchor this object to itself.");
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
            set
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
            get { return CurrentStyle.Size.Height; }
        }

        public virtual int Width
        {
            get { return CurrentStyle.Size.Width; }
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
                return CurrentStyle.HoverOffset.ApplyToRectangle(Position, CurrentStyle.Size);
            }
        }

        public virtual Rectangle DraggableBounds
        {
            get
            {
                return CurrentStyle.DraggableOffset.ApplyToRectangle(Position, CurrentStyle.Size);
            }
        }

        public virtual Rectangle InteractiveBounds
        {
            get
            {
                return CurrentStyle.InteractiveOffset.ApplyToRectangle(Position, CurrentStyle.Size);
            }
        }
        #endregion


        #region [ Style ]

        public TextureControlStyle DefaultStyle { get; set; }
        public TextureControlStyle HoveredStyle { get; set; }
        public TextureControlStyle PressedStyle { get; set; }


        private TextureControlStyle _currentStyle;
        public TextureControlStyle CurrentStyle
        {
            get { return _currentStyle; }
            set
            {
                if (value != _currentStyle)
                {
                    //TODO: Is there really a use case in which the anchor dimmensions 
                    // of the control change based on the curent style of it?
                    if (!_currentStyle.EqualDimmensionsTo(value))
                        DimmensionChanged?.Invoke(this, EventArgs.Empty);
                    _currentStyle = value;
                }
            }
        }
        #endregion


        #region [ Events ]
        public event EventHandler MouseEntered;
        public event EventHandler MouseLeft;
        public event EventHandler MouseDown;
        public event EventHandler MouseUp;
        public event EventHandler DimmensionChanged;
        public event EventHandler Clicked;
        #endregion


        #region [ State ]
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public bool Pressed { get; protected set; }
        public bool Hovered { get; protected set; }
        #endregion


        #region [ Mouse Down/Up ]
        public virtual void OnMouseDown(MouseEventArgs e)
        {
            if (Enabled && !Pressed)
            {
                if (InteractiveBounds.Contains(e.Position))
                {
                    Pressed = true;
                    if (PressedStyle != null)
                        CurrentStyle = PressedStyle;
                    MouseDown?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
            if (Enabled && Pressed)
            {
                if (InteractiveBounds.Contains(e.Position))
                {
                    Pressed = false;
                    CurrentStyle = DefaultStyle;
                    MouseUp?.Invoke(this, EventArgs.Empty);
                    Clicked?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion


        #region [ Mouse Hover ]
        public virtual void OnMouseEntered(MouseEventArgs e)
        {
            if (Enabled && !Hovered)
            {
                if (HoverBounds.Contains(e.Position))
                {
                    Hovered = true;
                    if (HoveredStyle != null)
                        CurrentStyle = HoveredStyle;
                    MouseEntered?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public virtual void OnMouseLeft(MouseEventArgs e)
        {
            if (Enabled && Hovered)
            {
                if (!HoverBounds.Contains(e.Position))
                {
                    Hovered = false;
                    CurrentStyle = DefaultStyle;
                    MouseLeft?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion


        #region [ Virtual - Update / Draw ]
        //public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        #endregion
    }
}
