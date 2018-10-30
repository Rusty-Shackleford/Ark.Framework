using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark.Framework.GUI.Anchoring
{
    public class AnchorToArgs : EventArgs
    {
        #region [ Constructor ]
        public AnchorToArgs(IAnchorable anchorTo) 
            : this(anchorTo, PositionType.Inside_Top_Left, 0, 0 , AnchorType.Bounds){ }

        public AnchorToArgs(IAnchorable anchorTo, PositionType positionType, int x, int y, AnchorType type)
        {
            AnchorTo = anchorTo;
            PositionType = positionType;
            OffsetX = x;
            OffsetY = y;
            AnchorType = type;
        }
        #endregion


        #region [ Members ]
        public IAnchorable AnchorTo { get; set; }
        public PositionType PositionType { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public AnchorType AnchorType { get; set; }
        #endregion
    }
}
