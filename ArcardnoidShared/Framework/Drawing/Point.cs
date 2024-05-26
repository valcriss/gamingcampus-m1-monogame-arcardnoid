namespace ArcardnoidShared.Framework.Drawing
{
    public class Point
    {
        #region Public Properties

        public static Point Zero => new(0, 0);
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

        public static Point operator *(float a, Point b)
        {
            return new Point(a * b.X, a * b.Y);
        }

        public static float operator *(Point a, Point b)
        {
            return a.X * b.X + a.Y * b.Y;
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

        public override bool Equals(object? obj)
        {
            return (obj != null && (Point)obj == this);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Point Normalize()
        {
            return Point.Normalize(this);
        }

        public Point Reflect(Point planeVector)
        {
            // calculate the projection of this vector on the plane vector
            Point projection = (this * planeVector) / (planeVector * planeVector) * planeVector;
            // calculate the reflection vector
            return this - 2f * projection;
        }

        public Point Rotate(float angle)
        {
            float x = X * (float)Math.Cos(angle) - Y * (float)Math.Sin(angle);
            float y = X * (float)Math.Sin(angle) + Y * (float)Math.Cos(angle);
            return new Point(x, y);
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }

        #endregion Public Methods
    }
}