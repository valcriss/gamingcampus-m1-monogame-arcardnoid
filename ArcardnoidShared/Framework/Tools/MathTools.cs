using ArcardnoidShared.Framework.Drawing;

namespace ArcardnoidShared.Framework.Tools
{
    public static class MathTools
    {
        #region Public Methods

        public static float AngleBetweenTwoPoints(Point point1, Point point2)
        {
            return RadToDeg((float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X));
        }

        public static float Clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static float DegToRad(float deg)
        {
            return deg * ((float)Math.PI / 180);
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static float RadToDeg(float rad)
        {
            return rad * (180 / (float)Math.PI);
        }

        public static Point VectorBetweenTwoPoints(Point point1, Point point2)
        {
            float angle = AngleBetweenTwoPoints(point1, point2);
            return VectorFromAngle(angle);
        }

        public static Point VectorFromAngle(float angle)
        {
            return new Point((float)Math.Cos(DegToRad(angle)), (float)Math.Sin(DegToRad(angle)));
        }

        #endregion Public Methods
    }
}