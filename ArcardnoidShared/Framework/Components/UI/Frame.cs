using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidShared.Framework.Components.UI
{
    public class Frame : GameComponent
    {
        #region Private Properties

        private Rectangle Bottom { get; set; } = Rectangle.Empty;
        private Rectangle BottomLeft { get; set; } = Rectangle.Empty;
        private Rectangle BottomRight { get; set; } = Rectangle.Empty;
        private Rectangle Center { get; set; } = Rectangle.Empty;
        private TextureType FrameAsset { get; set; }
        private Rectangle Left { get; set; } = Rectangle.Empty;
        private List<Rectangle[]> Rectangles { get; set; } = new List<Rectangle[]>();
        private Rectangle Right { get; set; } = Rectangle.Empty;
        private ITexture? Texture { get; set; }
        private Rectangle Top { get; set; } = Rectangle.Empty;
        private Rectangle TopLeft { get; set; } = Rectangle.Empty;
        private Rectangle TopRight { get; set; } = Rectangle.Empty;

        #endregion Private Properties

        #region Public Constructors

        public Frame(TextureType frameAsset, int x = 0, int y = 0, int width = 0, int height = 0) : base(x, y, width, height)
        {
            FrameAsset = frameAsset;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            base.Draw();
            foreach (Rectangle[] rectangle in Rectangles)
            {
                Texture?.DrawTexture(rectangle[1], rectangle[0], Color, 0, Point.Zero);
            }
        }

        public override void Load()
        {
            base.Load();
            Texture = GameServiceProvider.GetService<ITextureService>().Load(FrameAsset);
            int width = Texture.Width / 3;
            int height = Texture.Height / 3;

            TopLeft = new Rectangle(0, 0, width, height);
            TopRight = new Rectangle(2 * width, 0, width, height);
            BottomLeft = new Rectangle(0, 2 * height, width, height);
            BottomRight = new Rectangle(2 * width, 2 * height, width, height);
            Top = new Rectangle(width, 0, width, height);
            Bottom = new Rectangle(width, 2 * height, width, height);
            Left = new Rectangle(0, height, width, height);
            Right = new Rectangle(2 * width, height, width, height);
            Center = new Rectangle(width, height, width, height);
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            if (Texture == null) return;
            int width = Texture.Width / 3;
            int height = Texture.Height / 3;
            Rectangles = new List<Rectangle[]>
            {
                new Rectangle[] { TopLeft, new((int)RealBounds.X, (int)RealBounds.Y, width, height) }
            };
            for (int x = (int)RealBounds.X + width; x < RealBounds.X + RealBounds.Width - width; x += width)
            {
                Rectangles.Add(new Rectangle[] { Top, new(x, (int)RealBounds.Y, width, height) });
            }
            Rectangles.Add(new Rectangle[] { TopRight, new((int)RealBounds.X + (int)RealBounds.Width - width, (int)RealBounds.Y, width, height) });
            for (int y = (int)RealBounds.Y + height; y < Math.Floor(RealBounds.Y + RealBounds.Height - height); y += height)
            {
                Rectangles.Add(new Rectangle[] { Left, new((int)RealBounds.X, y, width, height) });
                for (int x = (int)RealBounds.X + width; x < RealBounds.X + RealBounds.Width - width; x += width)
                {
                    Rectangles.Add(new Rectangle[] { Center, new(x, y, width, height) });
                }
                Rectangles.Add(new Rectangle[] { Right, new((int)RealBounds.X + (int)RealBounds.Width - width, y, width, height) });
            }
            Rectangles.Add(new Rectangle[] { BottomLeft, new((int)RealBounds.X, (int)RealBounds.Y + (int)RealBounds.Height - height, width, height) });
            for (int x = (int)RealBounds.X + width; x < RealBounds.X + RealBounds.Width - width; x += width)
            {
                Rectangles.Add(new Rectangle[] { Bottom, new(x, (int)RealBounds.Y + (int)RealBounds.Height - height, width, height) });
            }
            Rectangles.Add(new Rectangle[] { BottomRight, new((int)RealBounds.X + (int)RealBounds.Width - width, (int)RealBounds.Y + (int)RealBounds.Height - height, width, height) });
        }

        #endregion Public Methods
    }
}