using Ark.Framework.GUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;


namespace Ark.Framework.Demo
{
    public static class GameAssets
    {
        #region [ Fonts ]
        public static BitmapFont Plumbis_11 { get; private set; }


        private static void LoadFonts(ContentManager content)
        {
            Plumbis_11 = content.Load<BitmapFont>(@"Fonts/Plumbis_11");
        }
        #endregion


        #region [ Texture Assets ]
        public static Texture2D Background { get; private set; }
        public static Texture2D MouseCursor { get; private set; }

        /// <summary>
        /// Load Textures
        /// </summary>
        private static void LoadTextures(ContentManager content)
        {
            Background = content.Load<Texture2D>(@"Backgrounds/background");
            MouseCursor = content.Load<Texture2D>(@"UI/MouseCursor");
        }


        #endregion


        #region [ UI Assets ]
        public static ControlStyle BtnStyle { get; private set; }
        public static ControlStyle BtnHoverStyle { get; private set; }
        public static ControlStyle BtnPressedStyle { get; private set; }
        public static PanelControlStyle PanelStyle { get; private set; }
        public static ControlStyle Ck_Style { get; private set; }
        public static ControlStyle Ck_Checked_Sty { get; private set; }
        public static ControlStyle Ck_Hovered_Sty { get; private set; }
        public static ControlStyle Ck_Checked_Hovered_Sty { get; private set; }

        private static void LoadUIAssets(ContentManager content)
        {
            var btn = content.Load<Texture2D>(@"UI/Button");
            var btnHover = content.Load<Texture2D>(@"UI/ButtonHovered");
            var btnPressed = content.Load<Texture2D>(@"UI/ButtonPressed");
            BtnStyle = new ControlStyle(btn)
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };
            BtnHoverStyle = new ControlStyle(btnHover)
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };
            BtnPressedStyle = new ControlStyle(btnPressed)
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };

            // PANEL
            var panelTexture = content.Load<Texture2D>(@"UI/Panel");
            PanelStyle = new PanelControlStyle(panelTexture, new RectangleOffset(-4, -24, -4, 0))
            {
                AnchoringOffset = new RectangleOffset(-4, 0, -4, -9),
                DraggableOffset = new RectangleOffset(-4, 0, -4, -476),
                Font = Plumbis_11,
                FontColor = Color.White
            };

            // Checkbox:
            var checkTexture = content.Load<Texture2D>(@"UI/Checkbox");
            var checkedTexture = content.Load<Texture2D>(@"UI/CheckboxChecked");
            var checkHoverTexture = content.Load<Texture2D>(@"UI/CheckboxHovered");
            var checkCheckedHoveredTexture = content.Load<Texture2D>(@"UI/CheckboxCheckedHovered");
            Ck_Style = new ControlStyle(checkTexture)
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };
            Ck_Checked_Sty = new ControlStyle(checkedTexture);
            Ck_Hovered_Sty = new ControlStyle(checkHoverTexture);
            Ck_Checked_Hovered_Sty = new ControlStyle(checkCheckedHoveredTexture);
        }
        #endregion


        #region [ LoadContent ]
        public static void LoadContent(ContentManager content)
        {
            LoadFonts(content);
            LoadTextures(content);
            LoadUIAssets(content);
        }
        #endregion
    }
}
