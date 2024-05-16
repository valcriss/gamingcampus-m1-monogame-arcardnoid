using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidShared.Framework.Components.UI
{
    public class Frame : GameComponent
    {
        private Rectangle Bottom { get; set; }
        private Rectangle BottomLeft { get; set; }
        private Rectangle BottomRight { get; set; }
        private Rectangle Center { get; set; }
        private string FrameAsset { get; set; }
        private Rectangle Left { get; set; }
        private List<Rectangle[]> Rectangles { get; set; } = new List<Rectangle[]>();
        private Rectangle Right { get; set; }
        private ITexture Texture { get; set; }
        private Rectangle Top { get; set; }
        private Rectangle TopLeft { get; set; }
        private Rectangle TopRight { get; set; }

        public Frame(string frameAsset, int x = 0, int y = 0, int width = 0, int height = 0) : base(x, y, width, height)
        {
            FrameAsset = frameAsset;
        }

        public override void Draw()
        {
            base.Draw();
            foreach (Rectangle[] rectangle in Rectangles)
            {
                Texture.DrawTexture(rectangle[1], rectangle[0], Color, 0, Point.Zero);
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
            int width = Texture.Width / 3;
            int height = Texture.Height / 3;
            Rectangles = new List<Rectangle[]>();

            Rectangles.Add(new Rectangle[] { TopLeft, new Rectangle((int)RealBounds.X, (int)RealBounds.Y, width, height) });
            for (int x = (int)RealBounds.X + width; x < RealBounds.X + RealBounds.Width - width; x += width)
            {
                Rectangles.Add(new Rectangle[] { Top, new Rectangle(x, (int)RealBounds.Y, width, height) });
            }
            Rectangles.Add(new Rectangle[] { TopRight, new Rectangle((int)RealBounds.X + (int)RealBounds.Width - width, (int)RealBounds.Y, width, height) });
            for (int y = (int)RealBounds.Y + height; y < Math.Floor(RealBounds.Y + RealBounds.Height - height); y += height)
            {
                Rectangles.Add(new Rectangle[] { Left, new Rectangle((int)RealBounds.X, y, width, height) });
                for (int x = (int)RealBounds.X + width; x < RealBounds.X + RealBounds.Width - width; x += width)
                {
                    Rectangles.Add(new Rectangle[] { Center, new Rectangle(x, y, width, height) });
                }
                Rectangles.Add(new Rectangle[] { Right, new Rectangle((int)RealBounds.X + (int)RealBounds.Width - width, y, width, height) });
            }
            Rectangles.Add(new Rectangle[] { BottomLeft, new Rectangle((int)RealBounds.X, (int)RealBounds.Y + (int)RealBounds.Height - height, width, height) });
            for (int x = (int)RealBounds.X + width; x < RealBounds.X + RealBounds.Width - width; x += width)
            {
                Rectangles.Add(new Rectangle[] { Bottom, new Rectangle(x, (int)RealBounds.Y + (int)RealBounds.Height - height, width, height) });
            }
            Rectangles.Add(new Rectangle[] { BottomRight, new Rectangle((int)RealBounds.X + (int)RealBounds.Width - width, (int)RealBounds.Y + (int)RealBounds.Height - height, width, height) });
        }
    }
}
