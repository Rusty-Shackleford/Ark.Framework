using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.Framework.GUI.Controls.Styles
{
    /// <summary>
    /// Generic style information used to render a Control.
    /// </summary>
    public class ControlStyle
    {

        #region [ Members ]
        public Texture2D Texture { get; set; }
        public Size Size { get { return new Size(Texture.Width, Texture.Height); } }
        public Rectangle InteractiveBounds { get; set; }
        public Rectangle DraggableBounds { get; set; }
        public Rectangle HoverBounds { get; set; }
        #endregion


        #region [ Constructor ]
        public ControlStyle(Texture2D texture)
        {
            Texture = texture;
        }
        #endregion

    }
}
