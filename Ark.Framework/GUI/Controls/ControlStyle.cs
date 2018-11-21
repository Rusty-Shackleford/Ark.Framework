using Ark.Framework.GUI.Anchoring;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System.ComponentModel;

namespace Ark.Framework.GUI.Controls
{
    /// <summary>
    /// Generic style information used to render a Control.
    /// </summary>
    public class ControlStyle
    {
        #region [ Members ]
        public string Name { get; set; }
        public BitmapFont Font { get; set; }
        public Color FontColor { get; set; }
        public Texture2D Texture { get; set; }
        public Size Size { get { return new Size(Texture.Width, Texture.Height); } }

        public RectangleOffset InteractiveOffset { get; set; }
        public RectangleOffset DraggableOffset { get; set; }
        public RectangleOffset HoverOffset { get; set; }
        public RectangleOffset AnchoringOffset { get; set; }

        public bool Initialized { get; private set; }

        public PositionOffset LabelOffset { get; set; }
        public AnchorAlignment LabelAlignment { get; set; }
        #endregion

        #region [ Constructor ]
        public ControlStyle(Texture2D texture)
        {
            Texture = texture;
            InteractiveOffset = RectangleOffset.Zero;
            DraggableOffset = RectangleOffset.Zero;
            HoverOffset = RectangleOffset.Zero;
            AnchoringOffset = RectangleOffset.Zero;

            LabelOffset = PositionOffset.Zero;
            LabelAlignment = AnchorAlignment.Inside_Middle_Center;
        }
        #endregion

        #region [ EqualSizeTo ]
        public bool EqualDimmensionsTo(ControlStyle other)
        {
            return Size == other.Size &&
                InteractiveOffset == other.InteractiveOffset &&
                DraggableOffset == other.DraggableOffset &&
                HoverOffset == other.HoverOffset &&
                AnchoringOffset == other.AnchoringOffset;
        }
        #endregion


        #region [ ToString ]
        public override string ToString()
        {
            string myType = GetType().Name;
            return $"{myType} | {Name} | Texture: {Texture.Name}";
        }
        #endregion
    }
}
