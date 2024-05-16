using ArcardnoidShared.Framework.Drawing;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models
{
    public static class DrawingExtensions
    {
        public static Color ToXnaColor(this GameColor color)
        {
            return Color.FromNonPremultiplied(color.R, color.G, color.B, color.A);
        }

        public static Vector2 ToVector2(this ArcardnoidShared.Framework.Drawing.Point point)
        {
            return new Vector2(point.X, point.Y);
        }

        public static RectangleF ToXnaRectangle(this ArcardnoidShared.Framework.Drawing.Rectangle rectangle)
        {
            return new RectangleF((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
        }
    }
}
