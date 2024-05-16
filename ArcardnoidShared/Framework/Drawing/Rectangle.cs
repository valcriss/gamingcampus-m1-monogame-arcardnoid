namespace ArcardnoidShared.Framework.Drawing
{
    public class Rectangle
    {
        #region Public Properties

        public float Height { get; set; }

        public Point Position => new Point(X, Y);

        public float Width { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        #endregion Public Properties

        #region Public Fields

        public static Rectangle Empty = new Rectangle(0, 0, 0, 0);

        #endregion Public Fields

        #region Public Constructors

        public Rectangle()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
        }

        public Rectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        #endregion Public Constructors

        #region Public Methods

        public bool Contains(Point point)
        {
            return point.X >= X && point.X <= X + Width && point.Y >= Y && point.Y <= Y + Height;
        }

        public void SetPosition(Point position)
        {
            X = position.X;
            Y = position.Y;
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, Width: {Width}, Height: {Height}";
        }

        #endregion Public Methods
    }
}