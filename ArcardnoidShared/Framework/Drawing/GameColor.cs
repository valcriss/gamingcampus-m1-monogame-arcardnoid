using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidShared.Framework.Drawing
{
    public struct GameColor
    {
        #region Public Properties

        public static GameColor Black => new(0, 0, 0);
        public static GameColor Blue => new(0, 0, 255);
        public static GameColor Cyan => new(0, 255, 255);
        public static GameColor Green => new(0, 255, 0);
        public static GameColor LightBlue => new(173, 216, 230);
        public static GameColor LightRed => new(241, 195, 203);
        public static GameColor Purple => new(128, 0, 128);
        public static GameColor Red => new(255, 0, 0);
        public static GameColor White => new(255, 255, 255);
        public static GameColor Yellow => new(255, 255, 0);
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

        public static GameColor Lerp(GameColor a, GameColor b, float t)
        {
            return new GameColor((int)MathTools.Lerp(a.R, b.R, t), (int)MathTools.Lerp(a.G, b.G, t), (int)MathTools.Lerp(a.B, b.B, t), (int)MathTools.Lerp(a.A, b.A, t));
        }

        public readonly GameColor UpdateOpacity(int a)
        {
            return new GameColor(R, G, B, a);
        }

        #endregion Public Methods
    }
}