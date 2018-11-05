using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark.Framework
{
    public class PositionOffset
    {

        #region [ Members ]
        public float X { get; set; }
        public float Y { get; set; }
        #endregion


        #region [ Constructor ]
        public PositionOffset(float x, float y)
        {
            X = x;
            Y = y;
        }
        public static PositionOffset Zero { get { return new PositionOffset(0, 0); } }
        #endregion


        public override string ToString()
        {
            return $"<{X.ToString()},{Y.ToString()}>";
        }

    }
}
