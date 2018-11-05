using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark.Framework
{
    public static class DebugHelp
    {
        public static string Vector2_ToString(Vector2 vector2)
        {
            return $"<{vector2.X.ToString()},{vector2.Y.ToString()}>";
        }

        public static string Rectangle_ToString(Rectangle rect)
        {
            return $"<{rect.X.ToString()},{rect.Y.ToString()},{rect.Width.ToString()},{rect.Height.ToString()}>";
        }
    }
}
