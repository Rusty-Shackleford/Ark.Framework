using Ark.Framework.GUI.Anchoring;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark.Framework
{
    /// <summary>
    /// A clipping plane used to describe a viewable area of a render-target surface.
    /// </summary>
    public class Viewport : IAnchorable
    {
        #region [ Members ]
        public string Name { get; set; }

        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                if (value != _position)
                {
                    Vector2 distanceMoved = Vector2.Subtract(value, _position);
                    PositionChanged?.Invoke(this, new AnchorMovedArgs(distanceMoved));
                    _position = value;
                }
            }
        }

        // TODO: Size should update anchors
        public Size Size { get; private set; }
        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Size.Width, Size.Height); }
        }
        #endregion


        #region [ Anchoring ]
        public bool Anchored { get; private set; }
        public Rectangle GetAnchorBounds() { return Bounds; }
        public event EventHandler<AnchorMovedArgs> PositionChanged;
        public event EventHandler<AnchorResizedArgs> Resized;
        private AnchorComponent Anchor { get; set; }

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


        #region [ Constructor ]
        public Viewport(Size size)
        {
            Size = size;
        }


        public Viewport(int x, int y, int width, int height)
        {
            Position = new Vector2(x, y);
            Size = new Size(width, height);
        }

        public Viewport(Vector2 position, Size size)
        {
            Position = position;
            Size = size;
        }
        #endregion
    }
}
