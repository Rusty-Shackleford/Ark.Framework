﻿using Ark.Framework.GUI;
using Ark.Framework.GUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Ark.Framework.GUI.Anchoring;
using System;
using System.Diagnostics;

namespace Ark.Framework.Demo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ArkDemoBase : Game
    {
        #region [ Members ]
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }

        int _screenWidth;
        int _screenHeight;
        bool _screenSizeChanged;
        #endregion


        #region [ Constructor ]
        public ArkDemoBase()
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
            Window.Title = "Ark Demo";

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
        }
        #endregion


        #region [ UnloadContent ]
        protected override void UnloadContent()
        {
            // UNLOAD HERE:
        }
        #endregion


        #region [ Update ]
        protected override void Update(GameTime gameTime)
        {
            // Screen Size Changes:
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
            // UPDATE HERE:

            base.Update(gameTime);
        }
        #endregion


        #region [ Draw ]
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(32, 32, 32));
            _spriteBatch.Begin();
            // DRAW HERE:


            // END DRAW:
            // Mouse Cursor:  Last to draw to be on top
            _spriteBatch.Draw(GameAssets.MouseCursor, 
                new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
    }
}
