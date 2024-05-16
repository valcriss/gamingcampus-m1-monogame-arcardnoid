using ArcardnoidShared.Framework.Drawing;

namespace ArcardnoidShared.Framework.Tools
{
    public static class ScreenManager
    {
        #region Private Fields

        private const float DEVELOPMENT_HEIGHT = 1080;
        private const float DEVELOPMENT_WIDTH = 1920;
        private static float _heightRatio;
        private static float _widthRatio;

        #endregion Private Fields

        #region Public Methods

        public static void Initialize(int width, int height)
        {
            _widthRatio = width / DEVELOPMENT_WIDTH;
            _heightRatio = height / DEVELOPMENT_HEIGHT;
        }

        public static Point Scale(Point position)
        {
            return new Point(position.X * _widthRatio, position.Y * _heightRatio);
        }

        public static Rectangle Scale(Rectangle rectangle)
        {            
            return new Rectangle(rectangle.X * _widthRatio, rectangle.Y * _heightRatio, rectangle.Width * _widthRatio,rectangle.Height * _heightRatio);
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

        public static float ScaleY(float value)
        {
            return value * _heightRatio;
        }

        public static int ScaleY(int value)
        {
            return (int)MathF.Round(ScaleY((float)value));
        }

        public static Point UIScale(Point position)
        {
            return new Point((int)(position.X / _widthRatio), (int)(position.Y / _heightRatio));
        }

        #endregion Public Methods
    }
}