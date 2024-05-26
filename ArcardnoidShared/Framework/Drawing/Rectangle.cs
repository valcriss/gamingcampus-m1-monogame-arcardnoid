namespace ArcardnoidShared.Framework.Drawing
{
    public class Rectangle
    {
        #region Public Properties

        public Point BottomLeft => new(X, Y + Height);
        public Point BotttomRight => new(X + Width, Y + Height);
        public float Height { get; set; }

        public Point Position => new(X, Y);

        public Point TopLeft => new(X, Y);
        public Point TopRight => new(X + Width, Y);
        public float Width { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        #endregion Public Properties

        #region Public Fields

        public static readonly Rectangle Empty = new(0, 0, 0, 0);

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

        public RectangleFace Collide(Rectangle rectangle)
        {
            if (Contains(rectangle.TopLeft) || Contains(rectangle.TopRight))
                return RectangleFace.Top;
            if (Contains(rectangle.BottomLeft) || Contains(rectangle.BotttomRight))
                return RectangleFace.Bottom;
            if (Contains(rectangle.TopLeft) || Contains(rectangle.BottomLeft))
                return RectangleFace.Left;
            if (Contains(rectangle.TopRight) || Contains(rectangle.BotttomRight))
                return RectangleFace.Right;
            return RectangleFace.None;
        }

        public RectangleFace Collide(Point point)
        {
            if (!Contains(point))
                return RectangleFace.None;

            Dictionary<RectangleFace, float> dict = new Dictionary<RectangleFace, float>();

            dict.Add(RectangleFace.Left, point.X - X);
            dict.Add(RectangleFace.Right, X + Width - point.X);
            dict.Add(RectangleFace.Top, point.Y - Y);
            dict.Add(RectangleFace.Bottom, Y + Height - point.Y);

            return dict.Count > 0 ? dict.OrderBy(x => x.Value).First().Key : RectangleFace.None;
        }

        public bool Contains(Point point)
        {
            return point.X >= X && point.X <= X + Width && point.Y >= Y && point.Y <= Y + Height;
        }

        public bool Intersects(Rectangle rectangle)
        {
            return Contains(rectangle.TopLeft) ||
                   Contains(rectangle.TopRight) ||
                   Contains(rectangle.BottomLeft) ||
                   Contains(rectangle.BotttomRight);
        }

        public Rectangle Scale(float size)
        {
            float width = Width * size;
            float height = Height * size;
            float x = X - (width / 2);
            float y = Y - (height / 2);
            return new Rectangle(x, y, width, height);
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