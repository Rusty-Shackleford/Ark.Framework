using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using System;


namespace Ark.Framework.Demo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ArkDemoGame : Game
    {
        #region [ Members ]
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }

        int _screenWidth;
        int _screenHeight;
        bool _screenSizeChanged;

                                                                                                                                        
        // Specific Junk
        Texture2D background { get; set; }

        #endregion


        #region [ Constructor ]
        public ArkDemoGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        #endregion


        #region [ Initialize ]
        protected override void Initialize()
        {
            ScreenWidth = 1024;
            ScreenHeight = 768;

            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();

            Window.IsBorderless = false;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;
            Window.Title = "Conway's Game Of Life";

            base.Initialize();
        }
        #endregion


        #region [ Event: Window_ClientSizeChanged ]
        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            _screenSizeChanged = true;
            _screenWidth = Window.ClientBounds.Width;
            _screenHeight = Window.ClientBounds.Height;
        }
        #endregion


        #region [ LoadContent ]
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameAssets.LoadContent(Content);

            Mouse.SetCursor(MouseCursor.FromTexture2D(GameAssets.MouseCursor, 0, 0));
            background = GameAssets.Background;
        }
        #endregion


        #region [ UnloadContent ]
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion


        #region [ Update ]
        protected override void Update(GameTime gameTime)
        {
            // SCREEN SIZE CHANGES
            if (_screenSizeChanged)
            {
                _graphics.PreferredBackBufferWidth = _screenWidth;
                _graphics.PreferredBackBufferHeight = _screenHeight;
                _graphics.ApplyChanges();
                _screenSizeChanged = false;

                ScreenWidth = _screenWidth;
                ScreenHeight = _screenHeight;

            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        #endregion


        #region [ Draw ]
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(32, 32, 32));

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            // Background:
            _spriteBatch.Draw(background, Vector2.Zero, Color.White);
            _spriteBatch.DrawString(GameAssets.Plumbis_11, "Test", new Vector2(100,100), Color.White);
            // GlobalSpriteBatch.Draw(background, Vector2.Zero, Color.White);

            // Mouse Cursor:  Last to draw in GlobalSpriteBatch
            // to ensure it is on top of z-position.
            DrawMouseCursor(_spriteBatch);


            // Last: SpriteBatch End and Call Base
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion


        #region [ DrawMouseCursor ]
        public void DrawMouseCursor(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameAssets.MouseCursor, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
        }
        #endregion
    }
}
