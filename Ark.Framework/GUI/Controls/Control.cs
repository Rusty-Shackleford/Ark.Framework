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
        /// <returns></returns>
        public abstract Control MakeClone();


        #region [ Constructor / Initialize ]
        /// <summary>
        /// Create a new control.  Call Initialize() to notify the control that its construction is completed.
        /// </summary>
        /// <param name="style">The default style to be used to render the control.</param>
        protected Control(ControlStyle style)
        {
            Visible = true;
            Enabled = true;
            DefaultStyle = style;
            _currentStyle = DefaultStyle;
            HoveredStyle = DefaultStyle;
            PressedStyle = DefaultStyle;
        }


        /// <summary>
        /// Re-apply state-dependent data on this control.
        /// </summary>
        public virtual void Initialize()
        {
            if (Anchor != null)
            {
                Position = Anchor.AnchoredPosition;
            }
            Initialized = true;
            Debug.WriteLine($"[{Name}]: Refresh()");
            Debug.WriteLine($"  > " + ToString());
        }

        /// <summary>
        /// Fetch any controls used as compoenents in the construction of this control,
        /// so that they can be managed by an InputHandler
        /// </summary>
        /// <returns>A list of all controls that need input handling.</returns>
        public virtual List<Control> RegisterSubControls() { return null; }
        #endregion



        #region [ Anchoring ]
        public abstract Rectangle GetAnchorBounds();
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
                    Vector2 distanceMoved = Vector2.Subtract(value, _position);
                    //var distanceMoved = Vector2.Distance(_position, value);
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

        public ControlStyle DefaultStyle { get; set; }
        public ControlStyle HoveredStyle { get; set; }
        public ControlStyle PressedStyle { get; set; }


        private ControlStyle _currentStyle;
        public ControlStyle CurrentStyle
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
