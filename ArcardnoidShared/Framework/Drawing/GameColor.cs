namespace ArcardnoidShared.Framework.Drawing
{
    public struct GameColor
    {
        #region Public Properties

        public static GameColor Black => new GameColor(0, 0, 0);
        public static GameColor White => new GameColor(255, 255, 255);
        public static GameColor Red => new GameColor(255, 0, 0);
        public static GameColor Cyan => new GameColor(0, 255, 255);
        public static GameColor Yellow => new GameColor(255, 255, 0);
        public int A { get; set; }
        public int B { get; set; }
        public int G { get; set; }
        public int R { get; set; }
        

        #endregion Public Properties

        #region Public Constructors

        public GameColor(int r, int g, int b, int a = 255)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        #endregion Public Constructors

        #region Public Methods

        public GameColor UpdateOpacity(int a)
        {
            return new GameColor(R, G, B, a);
        }

        #endregion Public Methods
    }
}