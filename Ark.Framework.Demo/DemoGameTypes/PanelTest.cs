using Ark.Framework.GUI;
using Ark.Framework.GUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Ark.Framework.GUI.Anchoring;
using System;
using System.Diagnostics;
using Ark.Framework.Graphics;

namespace Ark.Framework.Demo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class PanelTest : Game
    {
        #region [ Members ]
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }

        int _screenWidth;
        int _screenHeight;
        bool _screenSizeChanged;

        Panel TestPanel;
        int buttonClickedCount;

        Label LabelStatus { get; set; }
        Label LabelMoveStatus { get; set; }
        Label LabelViewportBounds { get; set; }
        Label LabelMousePosition { get; set; }


        TextureMaker TextureMaker;
        Sprite ViewportBorder;
        #endregion


        #region [ Constructor ]
        public PanelTest()
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

            TextureMaker = new TextureMaker(_graphics.GraphicsDevice);

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

            // PANEL:
            TestPanel = GameAssets.ConstructPanel("TestPanel", "Test Panel");
            TestPanel.MoveStarted += Panel_MoveStarted;
            TestPanel.MoveEnded += Panel_MoveEnded;
            TestPanel.Moved += Panel_Moved;

            ViewportBorder = new Sprite(TextureMaker.MakeBorder(Color.Red, 255, TestPanel.Viewport.Bounds));

            // LABELS:
            LabelStatus = new Label(GameAssets.BtnStyle, "Status Message");            
            TestPanel.Add(LabelStatus, AnchorPreference.Panel, new PositionOffset(-100, 35), AnchorAlignment.Inside_Top_Right);

            LabelMoveStatus = new Label(GameAssets.BtnStyle, "---");
            TestPanel.Add(LabelMoveStatus, AnchorPreference.Last, new PositionOffset(0, 10), AnchorAlignment.Below_Left);

            LabelViewportBounds = new Label(GameAssets.BtnStyle, "");
            TestPanel.Add(LabelViewportBounds, AnchorPreference.Last, new PositionOffset(0, 6), AnchorAlignment.Below_Left);

            LabelMousePosition = new Label(GameAssets.BtnStyle, "");
            TestPanel.Add(LabelMousePosition, AnchorPreference.Last, new PositionOffset(0, 6), AnchorAlignment.Below_Left);
            
            
            // BUTTON
            Button btn = GameAssets.ConstructButton("Btn1", "Click Me");

            btn.Clicked += Btn1_Clicked;
            TestPanel.Add(btn, AnchorPreference.Panel, new PositionOffset(20, 30), AnchorAlignment.Inside_Top_Left);


            Checkbox box = GameAssets.ConstructCheckbox("Ck1", "Not Checked");
            box.Checked += Ck1_Checked;
            box.Unchecked += Ck1_Unchecked;

            TestPanel.Add(box, AnchorPreference.Last, new PositionOffset(0, 20), AnchorAlignment.Below_Left);
        }
        #endregion


        #region [ Event Handlers ]
        private void Panel_MoveStarted(object sender, EventArgs e)
        {
            LabelMoveStatus.Text = "Panel Move Started";
        }

        private void Panel_MoveEnded(object sender, EventArgs e)
        {
            LabelMoveStatus.Text = "Panel Move Ended";
            LabelStatus.Text = "Stopped.";
        }

        private void Panel_Moved(object sender, EventArgs e)
        {
            LabelStatus.Text = "Panel Moving";
        }

        private void Ck1_Unchecked(object sender, EventArgs e)
        {
            Checkbox ck = (Checkbox)sender;
            ck.Text = "Not Checked";
        }


        private void Ck1_Checked(object sender, EventArgs e)
        {
            Checkbox ck = (Checkbox)sender;
            ck.Text = "Checked";
        }

        private void Btn1_Clicked(object sender, EventArgs e)
        {
            buttonClickedCount++;
            TestPanel.Text = $"Button Clicked {buttonClickedCount.ToString()} times";
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
            TestPanel.Update(gameTime);
            ViewportBorder.Position = TestPanel.Viewport.Position;

            string test = "Viewport: " + TestPanel.Viewport.Bounds.ToString() + "\n";
            test += "View Pos: " + TestPanel.Viewport.Position.ToString() + "\n";
            test += "Panel: " + TestPanel.Position.ToString() + "\n";
            test += "Mouse: " + Mouse.GetState().Position.ToString() + "\n";

            LabelViewportBounds.Text = test;

            base.Update(gameTime);
        }
        #endregion


        #region [ Draw ]
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(32, 32, 32));
            _spriteBatch.Begin();
            // DRAW HERE:
            TestPanel.Draw(_spriteBatch);

            ViewportBorder.Draw(_spriteBatch);
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
