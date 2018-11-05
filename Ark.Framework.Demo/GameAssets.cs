using Ark.Framework.GUI.Controls.Styles;
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


        #region [ Button Assets ]
        private static Texture2D ButtonTexture;
        private static Texture2D ButtonHoverTexture;
        private static Texture2D ButtonPressedTexture;

        private static void LoadButtonAssets(ContentManager content)
        {
            ButtonTexture = content.Load<Texture2D>(@"UI/Button");
            ButtonHoverTexture = content.Load<Texture2D>(@"UI/ButtonHover");
            ButtonPressedTexture = content.Load<Texture2D>(@"UI/ButtonPressed");
            Button = new ControlStyle(ButtonTexture)
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };
            ButtonHover = new ControlStyle(ButtonHoverTexture)
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };
            ButtonPressed = new ControlStyle(ButtonPressedTexture)
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };
        }

        public static ControlStyle Button { get; private set; }
        public static ControlStyle ButtonHover { get; private set; }
        public static ControlStyle ButtonPressed { get; private set; }
        #endregion

        #region [ LoadContent ]
        public static void LoadContent(ContentManager content)
        {
            LoadFonts(content);
            LoadTextures(content);
            LoadButtonAssets(content);
        }
        #endregion
    }
}
