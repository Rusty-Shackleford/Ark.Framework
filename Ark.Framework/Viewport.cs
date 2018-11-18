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
    public class Viewport
    {
        #region [ Members ]
        public Vector2 Position { get; set; }
        public Size Size { get; set; }
        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.Y, (int)Position.Y, Size.Width, Size.Height); }
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
