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
        public static ControlStyle Button { get; private set; }
        public static ControlStyle ButtonHover { get; private set; }
        public static ControlStyle ButtonPressed { get; private set; }
        public static ControlStyle PanelStyle { get; private set; }


        private static void LoadUIAssets(ContentManager content)
        {
            var btn = content.Load<Texture2D>(@"UI/Button");
            var btnHover = content.Load<Texture2D>(@"UI/ButtonHover");
            var btnPressed = content.Load<Texture2D>(@"UI/ButtonPressed");
            Button = new ControlStyle(btn)
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };
            ButtonHover = new ControlStyle(btnHover)
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };
            ButtonPressed = new ControlStyle(btnPressed)
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };

            // PANEL
            var panelTexture = content.Load<Texture2D>(@"UI/Panel");
            PanelStyle = new ControlStyle(panelTexture)
            {
                AnchoringOffset = new RectangleOffset(-4, 0, -4, -9),
                DraggableOffset = new RectangleOffset(-4, 0, -4, -476)
            };
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
