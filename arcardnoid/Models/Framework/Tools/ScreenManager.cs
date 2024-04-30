using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models.Framework.Tools
{
    public static class ScreenManager
    {
        private const float DEVELOPMENT_WIDTH = 1920;
        private const float DEVELOPMENT_HEIGHT = 1080;

        private static float _widthRatio;
        private static float _heightRatio;

        public static void Initialize(int width,int height)
        {
            _widthRatio = width / DEVELOPMENT_WIDTH;
            _heightRatio = height / DEVELOPMENT_HEIGHT;
        }

        public static Point UIScale(Point position)
        {
            return new Point((int)(position.X / _widthRatio), (int)(position.Y / _heightRatio));
        }

        public static Vector2 Scale(Vector2 position)
        {
            return new Vector2(position.X * _widthRatio, position.Y * _heightRatio);
        }

        public static Vector2 UIScale(Vector2 position)
        {
            return new Vector2(position.X / _widthRatio, position.Y / _heightRatio);
        }

        public static Rectangle Scale(Rectangle rectangle)
        {

            return Scale(rectangle.ToRectangleF()).ToRectangle();
        }

        public static RectangleF Scale(RectangleF rectangle)
        {
            return new RectangleF(MathF.Round(rectangle.X * _widthRatio), MathF.Round(rectangle.Y * _heightRatio), MathF.Round(rectangle.Width * _widthRatio), MathF.Round(rectangle.Height * _heightRatio));
        }

        public static float Scale()
        {
            return MathF.Max(_heightRatio, _widthRatio);
        }

        public static float ScaleX(float value)
        {
            return value * _widthRatio;
        }

        public static int ScaleX(int value)
        {
            return (int)MathF.Round(ScaleX((float)value));
        }

        public static float ScaleY(float value) { 
            return value * _heightRatio;
        }

        public static int ScaleY(int value)
        {
            return (int)MathF.Round(ScaleY((float)value));
        }
    }
}
