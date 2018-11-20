﻿using Ark.Framework.GUI.Anchoring;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using System;

namespace Ark.Framework.GUI.Controls
{
    public abstract class Control : IAnchorable, IRefresh
    {
        #region [ Constructor / Initialize ]
        /// <summary>
        /// Create a new control.  Call Refresh() to notify the control that its construction is completed.
        /// </summary>
        /// <param name="style">The default style to be used to render the control.</param>
        protected Control(ControlStyle style)
        {
            Position = Vector2.Zero;
            Visible = true;
            Enabled = true;
            DefaultStyle = style;
            _currentStyle = DefaultStyle;
            HoveredStyle = DefaultStyle;
            PressedStyle = DefaultStyle;

            LabelAlignment = style.LabelAlignment;
            LabelOffset = style.LabelOffset;
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
            Label?.Refresh();
            Initialized = true;
        }
        #endregion
        

        #region [ Anchoring ]
        public bool Anchored { get; private set; }
        protected AnchorComponent Anchor { get; private set; }
        public event EventHandler<AnchorMovedArgs> PositionChanged;
        public event EventHandler<AnchorResizedArgs> Resized;

        public virtual Rectangle GetAnchorBounds()
        {
            return GetCurrentStyle().AnchoringOffset.Apply(Position, GetCurrentStyle().Size);
        }

        public void AnchorTo(IAnchorable target, AnchorAlignment alignment, PositionOffset offset)
        {
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

        public virtual void OnPositionChanged(AnchorMovedArgs args)
        {
            PositionChanged?.Invoke(this, args);
        }

        public virtual void OnResized(AnchorResizedArgs args)
        {
            Resized?.Invoke(this, args);
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
                    OnPositionChanged(new AnchorMovedArgs(distanceMoved));
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


        #region [ Text Label ]
        public Label Label { get; protected set; }

        private AnchorAlignment _labelAlignment;
        public AnchorAlignment LabelAlignment
        {
            get { return _labelAlignment; }
            set
            {
                if (_labelAlignment != value)
                {
                    _labelAlignment = value;
                    Label?.Refresh();
                }
            }
        }

        private PositionOffset _labelOffset;
        public PositionOffset LabelOffset
        {
            get { return _labelOffset; }
            set
            {
                if (_labelOffset != value)
                {
                    _labelOffset = value;
                    Label?.Refresh();
                }
            }
        }
        
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value != _text)
                {
                    _text = value;
                    Label = new Label(DefaultStyle, _text);
                    Label.AnchorTo(this, LabelAlignment, LabelOffset);
                }
            }
        }


        
        #endregion


        #region [ Style ]
        public ControlStyle DefaultStyle { get; set; }
        public ControlStyle HoveredStyle { get; set; }
        public ControlStyle PressedStyle { get; set; }
        public ControlStyle HoveredPressedStyle { get; set; }

        // Current Style uses a decorator pattern for controls that want
        // to use a derived style. See:
        // https://stackoverflow.com/questions/5709034/does-c-sharp-support-return-type-covariance
        protected ControlStyle _currentStyle;
        protected abstract ControlStyle CurrentStyle();

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
            _currentStyle = style;
        }
        #endregion


        #region [ Events ]
        public event EventHandler MouseEntered;
        public event EventHandler MouseLeft;
        public event EventHandler MouseDown;
        public event EventHandler Clicked;
        #endregion


        #region [ State ]
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public bool Pressed { get; protected set; }
        public bool Hovered { get; protected set; }
        public bool Initialized { get; protected set; }
        public string Name { get; set; }

        //public abstract ControlState GetState();
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
