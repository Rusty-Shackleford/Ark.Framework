using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ark.Framework.GUI.Anchoring
{
    public static class Anchoring
    {
        public static Vector2 GetPosition(Rectangle anchor, Rectangle anchored, PositionType type, Vector2 offset)
        {
            if (anchor == anchored)
            {
                return anchored.Location.ToVector2();
            }
            switch (type)
            {
                case PositionType.Above_Left:
                    return new Vector2(
                        anchor.X + offset.X,
                        anchor.Y - anchored.Height + offset.Y
                        );
                case PositionType.Above_Center:
                    return new Vector2(
                        anchor.Right - anchor.Width / 2 - anchored.Width / 2 + offset.X,
                        anchor.Y - anchored.Height + offset.Y
                        );
                case PositionType.Above_Right:
                    return new Vector2(
                        anchor.Right - anchored.Width + offset.X,
                        anchor.Y - anchored.Height + offset.Y
                        );
                case PositionType.Below_Left:
                    return new Vector2(
                        anchor.X + offset.X,
                        anchor.Bottom + offset.Y
                        );
                case PositionType.Below_Center:
                    return new Vector2(
                        anchor.Right - anchor.Width / 2 - anchored.Width / 2 + offset.X,
                        anchor.Bottom + offset.Y
                        );
                case PositionType.Below_Right:
                    return new Vector2(
                        anchor.Right - anchored.Width + offset.X,
                        anchor.Bottom + offset.Y
                        );
                case PositionType.Inside_Top_Left:
                    return new Vector2(
                        anchor.X + offset.X,
                        anchor.Y + offset.Y
                        );
                case PositionType.Inside_Top_Center:
                    return new Vector2(
                        anchor.Right - anchor.Width / 2 - anchored.Width / 2 + offset.X,
                        anchor.Y + offset.Y
                        );
                case PositionType.Inside_Top_Right:
                    return new Vector2(
                        anchor.Right - anchored.Width + offset.X,
                        anchor.Y + offset.Y
                        );
                case PositionType.Inside_Middle_Left:
                    return new Vector2(
                        anchor.X + offset.X,
                        anchor.Bottom - anchor.Height / 2 - anchored.Height / 2 + offset.Y
                        );
                case PositionType.Inside_Middle_Center:
                    return new Vector2(
                        anchor.Right - anchor.Width / 2 - anchored.Width / 2 + offset.X,
                       anchor.Bottom - anchor.Height / 2 - anchored.Height / 2 + offset.Y
                        );
                case PositionType.Inside_Middle_Right:
                    return new Vector2(
                        anchor.Right + offset.X,
                        anchor.Bottom - anchor.Height / 2 - anchored.Height / 2 + offset.Y
                        );
                case PositionType.Inside_Bottom_Left:
                    return new Vector2(
                        anchor.X + offset.X,
                        anchor.Bottom + offset.Y - anchored.Height
                        );
                case PositionType.Inside_Bottom_Center:
                    return new Vector2(
                        anchor.Right - anchor.Width / 2 - anchored.Width / 2 + offset.X,
                        anchor.Bottom - anchored.Height + offset.Y
                        );
                case PositionType.Inside_Bottom_Right:
                    return new Vector2(
                        anchor.Right - anchored.Width + offset.X,
                        anchor.Bottom - anchored.Height + offset.Y
                        );
                case PositionType.Outside_Left_Top:
                    return new Vector2(
                        anchor.X - anchored.Width + offset.X,
                        anchor.Y + offset.Y
                        );
                case PositionType.Outside_Left_Middle:
                    return new Vector2(
                        anchor.X + offset.X,
                        anchor.Bottom - anchor.Height / 2 - anchored.Height / 2 + offset.Y
                        );
                case PositionType.Outside_Left_Bottom:
                    return new Vector2(
                        anchor.X + offset.X,
                        anchor.Bottom - anchored.Height + offset.Y
                        );
                case PositionType.Outside_Right_Top:
                    return new Vector2(
                        anchor.Right + offset.X,
                        anchor.Y + offset.Y
                        );
                case PositionType.Outside_Right_Middle:
                    return new Vector2(
                        anchor.Right + offset.X,
                        anchor.Bottom - anchor.Height / 2 - anchored.Height / 2 + offset.Y
                        );
                case PositionType.Outside_Right_Bottom:
                    return new Vector2(
                        anchor.Right + offset.X,
                        anchor.Bottom - anchored.Height + offset.Y
                        );
                default:
                    throw new NotSupportedException("PositionType not supported.");
            }
        }
    }
}
