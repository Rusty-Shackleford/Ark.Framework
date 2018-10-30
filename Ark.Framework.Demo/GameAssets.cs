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


        #region [ LoadContent ]
        public static void LoadContent(ContentManager content)
        {
            LoadFonts(content);
            LoadTextures(content);
        }
        #endregion
    }
}
