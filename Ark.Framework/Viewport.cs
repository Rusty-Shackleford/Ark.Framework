﻿using Ark.Framework.GUI.Anchoring;
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
        public Vector2 Position { get; set; }
        public Size Size { get; set; }
        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.Y, (int)Position.Y, Size.Width, Size.Height); }
        }
        #endregion


        #region [ Anchoring ]
        public bool Anchored { get; private set; }
        public Rectangle GetAnchorBounds() { return Bounds; }
        public event EventHandler<AnchorMovedArgs> OnPositionChanged;
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
        public Viewport(Rectangle bounds)
        {
            Position = new Vector2(bounds.X, bounds.Y);
            Size = new Size(bounds.Width, bounds.Height);
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
