using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark.Framework
{
    public class TextureRegion
    {
        #region [ Members ]
        public string Name { get; set; }
        public Texture2D Texture { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Vector2 Position => new Vector2(X, Y);
        public Rectangle Bounds => new Rectangle((int)X, (int)Y, Width, Height);
        public Size Size => new Size(Width, Height);
        #endregion


        #region [ Constructor ]
        public TextureRegion(Texture2D texture, Vector2 position, Size size, string name = "") : 
            this(name, texture, (int)position.X, (int)position.Y, size.Width, size.Height) { }

        public TextureRegion(Texture2D texture, Rectangle region, string name = "") 
            : this(name, texture, region.X, region.Y, region.Width, region.Height) { }

        public TextureRegion(string name, Texture2D texture, int x, int y, int width, int height)
        {
            Texture = texture;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        #endregion
    }
}
