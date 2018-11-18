using Ark.Framework.GUI.Anchoring;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ark.Framework.GUI.Controls
{
    public abstract class Control : IAnchorable, IRefresh
    {
        /// <summary>
        /// Make a new instance of this control with its same settings.
        /// </summary>
        /// <returns>A new instance of this control with similar settings.</returns>
        public abstract Control MakeClone();


        #region [ Constructor / Initialize ]
        /// <summary>
        /// Create a new control.  Call Refresh() to notify the control that its construction is completed.
        /// </summary>
        /// <param name="style">The default style to be used to render the control.</param>
        protected Control(ControlStyle style)
        {
            Visible = true;
            Enabled = true;
            DefaultStyle = style;
            currentStyle = DefaultStyle;
            HoveredStyle = DefaultStyle;
            PressedStyle = DefaultStyle;
        }


        /// <summary>
        /// Apply state-dependent rules on this control, safe to call multiple times.
        /// </summary>
        public virtual void Refresh()
        {
            if (Anchor != null)
            {
                Position = Anchor.AnchoredPosition;
            }
            Initialized = true;
        }
        #endregion
        

        #region [ Anchoring ]
        public bool Anchored { get; private set; }
        public abstract Rectangle GetAnchorBounds();
        public event EventHandler<AnchorMovedArgs> OnPositionChanged;
        protected AnchorComponent Anchor { get; private set; }

        public void AnchorTo(IAnchorable target, AnchorAlignment alignment, PositionOffset offset)
        {
            // never anchor to yourself, it's dangerous
            if (target.GetHashCode() != GetHashCode())
            {
                RemoveAnchor();
                Anchor = new AnchorComponent(target, this, alignment, offset);
                Position = Anchor.AnchoredPosition;
                Anchored = true;
                return;
            }
            throw new NotSupportedException("Cannot anchor an object to itself.");
        }

        public void AnchorTo(AnchorSettings settings)
        {
            AnchorTo(settings.Anchor, settings.Alignment, settings.Offset);
        }

        public void RemoveAnchor()
        {
            if (Anchor != null)
            {
                Anchor.RemoveAnchor();
                Anchor = null;
                Anchored = false;
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
                    Vector2 distanceMoved = Vector2.Subtract(value, _position);
                    //var distanceMoved = Vector2.Distance(_position, value);
                    OnPositionChanged?.Invoke(this, new AnchorMovedArgs(distanceMoved));
                    _position = value;
                }
            }
        }

        /// <summary>
        /// Width determined by CurrentStyle Size
        /// </summary>
        public virtual int Width
        {
            get { return GetCurrentStyle().Size.Width; }
        }

        /// <summary>
        /// Height determined by CurrentStyle Size
        /// </summary>
        public virtual int Height
        {
            get { return GetCurrentStyle().Size.Height; }
        }

        /// <summary>
        /// Default bounds detection for this control.
        /// </summary>
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

        /// <summary>
        /// Area of this control that is used for mouse over detection.
        /// </summary>
        public virtual Rectangle HoverBounds
        {
            get
            {
                return GetCurrentStyle().HoverOffset.Apply(Position, GetCurrentStyle().Size);
            }
        }

        /// <summary>
        /// Area of this control that can be held for dragging.
        /// </summary>
        public virtual Rectangle DraggableBounds
        {
            get
            {
                return GetCurrentStyle().DraggableOffset.Apply(Position, GetCurrentStyle().Size);
            }
        }

        /// <summary>
        /// Area of this control that can be clicked.
        /// </summary>
        public virtual Rectangle InteractiveBounds
        {
            get
            {
                return GetCurrentStyle().InteractiveOffset.Apply(Position, GetCurrentStyle().Size);
            }
        }
        #endregion


        #region [ Style ]
        public ControlStyle DefaultStyle { get; set; }
        public ControlStyle HoveredStyle { get; set; }
        public ControlStyle PressedStyle { get; set; }

        // Current Style uses a decorator pattern for controls that want
        // to use a derived style. See:
        // https://stackoverflow.com/questions/5709034/does-c-sharp-support-return-type-covariance
        protected ControlStyle currentStyle;
        public abstract ControlStyle CurrentStyle();

        /// <summary>
        /// Retrieves the style currently used by this control.
        /// </summary>
        /// <returns></returns>
        public ControlStyle GetCurrentStyle()
        {
            return CurrentStyle();
        }

        /// <summary>
        /// Set the current style of this control.
        /// </summary>
        /// <param name="style"></param>
        public void SetCurrentStyle(ControlStyle style)
        {
            currentStyle = style;
        }
        #endregion


        #region [ Events ]
        public event EventHandler MouseEntered;
        public event EventHandler MouseLeft;
        public event EventHandler MouseDown;
        public event EventHandler MouseUp;
        public event EventHandler Clicked;
        #endregion


        #region [ State ]
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public bool Pressed { get; protected set; }
        public bool Hovered { get; protected set; }
        public bool Initialized { get; protected set; }
        public string Name { get; set; }
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
                        SetCurrentStyle(PressedStyle);
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
                    SetCurrentStyle(DefaultStyle);
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
                        SetCurrentStyle(HoveredStyle);
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
                    SetCurrentStyle(DefaultStyle);
                    MouseLeft?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion


        #region [ Virtual - Update / Draw ]
        //public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        #endregion


        #region [ ToString ]
        public override string ToString()
        {
            string myType = GetType().Name;
            string anchor = Anchor?.ToString();
            if (anchor == null)
                anchor = "<null>";

            return $"[Type: {myType}|" +
                $"Name: {Name}|" +
                $"Position: {DebugHelp.Vector2_ToString(Position)}|" +
                $"Bounds: {DebugHelp.Rectangle_ToString(Bounds)}|" +
                $"Anchor: {anchor}|";
        }
        #endregion
    }
}
