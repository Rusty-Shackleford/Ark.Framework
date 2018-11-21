using Ark.Framework.GUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System;

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
        public static ControlStyle BtnHoveredPressedStyle { get; private set; }

        public static PanelControlStyle PanelStyle { get; private set; }

        public static ControlStyle Ck_Style { get; private set; }
        public static ControlStyle Ck_Checked_Style { get; private set; }
        public static ControlStyle Ck_Hovered_Style { get; private set; }
        public static ControlStyle Ck_CheckedHovered_Style { get; private set; }
        public static ControlStyle Ck_HoveredPressed_Style { get; private set; }
        public static ControlStyle Ck_HoveredPressedChecked_Style { get; private set; }

        private static void LoadUIAssets(ContentManager content)
        {
            #region [ Load Button Styles ]
            BtnStyle = new ControlStyle(content.Load<Texture2D>(@"UI/Button"))
            {
                Font = Plumbis_11,
                FontColor = Color.White,
                LabelAlignment = GUI.Anchoring.AnchorAlignment.Inside_Middle_Center,
                LabelOffset = PositionOffset.Zero
            };
            BtnHoverStyle = new ControlStyle(content.Load<Texture2D>(@"UI/ButtonHovered"))
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };
            BtnPressedStyle = new ControlStyle(content.Load<Texture2D>(@"UI/ButtonPressed"))
            {

            };
            BtnHoveredPressedStyle = new ControlStyle(content.Load<Texture2D>(@"UI/ButtonHoveredPressed"))
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };
            #endregion


            #region [ Load Panel Styles ]
            var panelTexture = content.Load<Texture2D>(@"UI/Panel");
            PanelStyle = new PanelControlStyle(panelTexture, new RectangleOffset(-4, -24, -4, 0))
            {
                AnchoringOffset = new RectangleOffset(-4, 0, -4, -9),
                DraggableOffset = new RectangleOffset(-4, 0, -4, -476),
                Font = Plumbis_11,
                FontColor = Color.White,
                LabelOffset = new PositionOffset(0, 5)
            };
            #endregion


            #region [ Load Checkbox Styles ]
            Ck_Style = new ControlStyle(content.Load<Texture2D>(@"UI/Checkbox"))
            {
                Font = Plumbis_11,
                FontColor = Color.White,
                LabelAlignment = GUI.Anchoring.AnchorAlignment.Outside_Right_Middle,
                LabelOffset = new PositionOffset(8, 0)
            };

            Ck_Checked_Style = new ControlStyle(content.Load<Texture2D>(@"UI/CheckboxChecked"))
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };

            Ck_Hovered_Style = new ControlStyle(content.Load<Texture2D>(@"UI/CheckboxHovered"))
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };

            Ck_CheckedHovered_Style = new ControlStyle(content.Load<Texture2D>(@"UI/CheckboxCheckedHovered"))
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };

            Ck_HoveredPressed_Style = new ControlStyle(content.Load<Texture2D>(@"UI/CheckboxHoveredPressed"))
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };

            Ck_HoveredPressedChecked_Style = new ControlStyle(content.Load<Texture2D>(@"UI/CheckboxHoveredPressedChecked"))
            {
                Font = Plumbis_11,
                FontColor = Color.White
            };
            #endregion

        }
        #endregion


        #region [ Control Construction ]
        // TODO: The refreshes after each creation may not be strictly necessary.
        // When time, determine if can be removed.


        #region [ ConstructCheckbox ]
        /// <summary>
        /// Create a new Checkbox instance with the style of this game's assets.
        /// </summary>
        /// <param name="name">Name Identifier</param>
        /// <param name="text">Optional Label Text</param>
        /// <returns></returns>
        public static Checkbox ConstructCheckbox(string name, string text = "")
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException($"Checkbox requires a name.");

            Checkbox ck = new Checkbox(Ck_Style, Ck_Checked_Style, Ck_CheckedHovered_Style, Ck_HoveredPressedChecked_Style)
            {
                HoveredStyle = Ck_Hovered_Style,
                PressedStyle = Ck_HoveredPressed_Style,
                HoveredPressedStyle = Ck_HoveredPressed_Style,
                Name = name,
                Text = text
            };
            ck.Refresh();
            return ck;
        }
        #endregion


        #region [ ConstructPanel  ]
        /// <summary>
        /// Create a new Panel instance with the style of this game's assets.
        /// </summary>
        /// <param name="name">Name Identifier</param>
        /// <param name="text">Optional Label Text</param>
        /// <returns></returns>
        public static Panel ConstructPanel(string name, string text = "")
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException($"Panel requires a name.");

            Panel p = new Panel(PanelStyle)
            {
                Name = name,
                Text = text
            };
            p.Refresh();
            return p;
        }
        #endregion


        #region [ ConstructButton ]
        /// <summary>
        /// Create a new Panel instance with the style of this game's assets.
        /// </summary>
        /// <param name="name">Name Identifier</param>
        /// <param name="text">Optional Label Text</param>
        /// <returns></returns>
        public static Button ConstructButton(string name, string text = "")
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException($"Button requires a name.");

            Button b = new Button(BtnStyle)
            {
                HoveredStyle = BtnHoverStyle,
                PressedStyle = BtnPressedStyle,
                HoveredPressedStyle = BtnHoveredPressedStyle,
                Name = name,
                Text = text
            };
            b.Refresh();
            return b;
        }
        #endregion

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
