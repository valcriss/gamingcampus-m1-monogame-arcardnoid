namespace ArcardnoidShared.Framework.Drawing
{
    public class Point
    {
        #region Public Properties

        public static Point Zero => new Point(0, 0);
        public float X { get; set; }
        public float Y { get; set; }

        #endregion Public Properties

        #region Public Constructors

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        #endregion Public Constructors

        #region Public Methods

        public static Point Normalize(Point point)
        {
            float length = (float)Math.Sqrt(point.X * point.X + point.Y * point.Y);
            return new Point(point.X / length, point.Y / length);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public static bool operator !=(Point a, Point b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        public static Point operator *(Point a, float b)
        {
            return new Point(a.X * b, a.Y * b);
        }

        public static Point operator /(Point a, float b)
        {
            return new Point(a.X / b, a.Y / b);
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }

        #endregion Public Methods
    }
}