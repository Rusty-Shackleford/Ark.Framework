using Ark.Framework.GUI;
using Ark.Framework.GUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Diagnostics;

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
        InputHandler inputHandler = new InputHandler();
        Button button1 { get; set; }
        Button button2 { get; set; }
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

           
            background = GameAssets.Background;


            button1 = new Button(GameAssets.Button, new Vector2(100, 100))
            {
                HoveredStyle = GameAssets.ButtonHover,
                PressedStyle = GameAssets.ButtonPressed,
                Name = "button1"
            };
            button1.Label.Text = "My Button Yo";
            button1.MouseDown += MouseDownTest;
            button1.MouseUp += MouseUpTest;
            button1.MouseEntered += MouseEnteredTest;
            button1.MouseLeft += MouseLeftTest;
            button1.Clicked += ClickedTest;


            button2 = (Button)button1.MakeClone();
            button2.Name = "button2";
            button2.Label.Text = "Move";
            button2.AnchorTo(button1, GUI.Anchoring.AnchorAlignment.Below_Center, new PositionOffset(0, 30));
            button2.Clicked += MoveButton1;

            inputHandler.Controls.Add(button1);
            inputHandler.Controls.Add(button2);
        }

        private void MoveButton1(object sender, EventArgs e)
        {
            button1.Position = new Vector2(button1.Position.X + 50, button1.Position.Y + 50);
        }

        private void ClickedTest(object sender, EventArgs e)
        {
            Debug.WriteLine("CLICKED!");
        }
        private void MouseLeftTest(object sender, EventArgs e)
        {
            Debug.WriteLine("Mouse Left");
        }

        private void MouseEnteredTest(object sender, EventArgs e)
        {
            Debug.WriteLine("Mouse Entered");
        }

        private void MouseUpTest(object sender, EventArgs e)
        {
            Debug.WriteLine("Mouse Up");
        }

        private void MouseDownTest(object sender, EventArgs e)
        {
            Debug.WriteLine("Mouse Down");
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
            inputHandler.Update(gameTime);

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

            button1.Draw(_spriteBatch);
            button2.Draw(_spriteBatch);
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
