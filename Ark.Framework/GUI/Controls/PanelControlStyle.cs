using Ark.Framework.GUI.Anchoring;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark.Framework.GUI.Controls
{
    public class PanelControlStyle : ControlStyle
    {
        #region [ Members ]
        public Size ViewportSize { get; set; }
        #endregion


        #region [ Constructor ]
        public PanelControlStyle(Texture2D texture, Size viewportSize) : base(texture)
        {
            ViewportSize = viewportSize;
            LabelOffset = PositionOffset.Zero;
            LabelAlignment = AnchorAlignment.Inside_Top_Center;
        }
        #endregion
    }
}
