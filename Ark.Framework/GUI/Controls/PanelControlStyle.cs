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
        public Viewport Viewport { get; set; }
        #endregion


        #region [ Constructor ]
        public PanelControlStyle(Texture2D texture, Viewport viewport) : base(texture)
        {
            Viewport = viewport;
        }
        #endregion
    }
}
