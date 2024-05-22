using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.UI
{
    public class AnimatedTitleBand : GameComponent
    {
        #region Private Properties

        private string BandAsset { get; set; }
        private ITexture? BandTexture { get; set; }
        private BitmapText? BitmapText { get; set; }
        private string CurrentText { get; set; }
        private double ElapsedTime { get; set; }
        private string FontAsset { get; set; }
        private List<Rectangle> InsideBounds { get; set; } = new List<Rectangle>();
        private Rectangle LeftBounds { get; set; } = Rectangle.Empty;
        private Rectangle RightBounds { get; set; } = Rectangle.Empty;
        private double Speed { get; set; }
        private string Text { get; set; }
        private GameColor TextColor { get; set; }

        #endregion Private Properties

        #region Private Fields

        private const int OffsetY = 24;

        #endregion Private Fields

        #region Public Constructors

        public AnimatedTitleBand(string bandAsset, string fontAsset, string text, double speed, int x, int y, GameColor textColor) : base(x, y)
        {
            BandAsset = bandAsset;
            FontAsset = fontAsset;
            TextColor = textColor;
            Text = text;
            CurrentText = string.Empty;
            Speed = speed;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            base.Draw();
            if (LeftBounds == null || RightBounds == null || InsideBounds == null || BandTexture == null) return;
            BandTexture.DrawTexture(LeftBounds, new Rectangle(0, 0, 64, 64), Color, 0, Point.Zero);
            foreach (var insideBound in InsideBounds)
            {
                BandTexture.DrawTexture(insideBound, new Rectangle(64, 0, 64, 64), Color, 0, Point.Zero);
            }
            BandTexture.DrawTexture(RightBounds, new Rectangle(128, 0, 64, 64), Color, 0, Point.Zero);
        }

        public override void Load()
        {
            base.Load();
            BandTexture = GameServiceProvider.GetService<ITextureService>().Load(BandAsset);
            BitmapText = AddGameComponent(new BitmapText(FontAsset, CurrentText, 0, 0, TextHorizontalAlign.Center, TextVerticalAlign.Center));
            BitmapText.Color = TextColor;
            ElapsedTime = 0;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            if (BitmapText == null) return;
            if (CurrentText.Length < Text.Length)
            {
                ElapsedTime += delta;
                if (ElapsedTime >= Speed)
                {
                    int index = CurrentText.Length;
                    CurrentText += Text[index];
                    BitmapText.SetText(CurrentText);
                    ElapsedTime = 0;
                }
                Point size = BitmapText.MeasureString();
                int numberOfCenters = (int)Math.Ceiling(size.X / 64);
                int sizeOfCenters = numberOfCenters * 64;
                LeftBounds = new Rectangle((int)Bounds.X - 64 - (sizeOfCenters / 2), (int)Bounds.Y - OffsetY, 64, 64);
                InsideBounds = new List<Rectangle>();
                for (int x = (int)Bounds.X - 64 - (sizeOfCenters / 2) + 64; x < (int)Bounds.X + (sizeOfCenters / 2); x = x + 64)
                {
                    InsideBounds.Add(new Rectangle(x, (int)Bounds.Y - OffsetY, 64, 64));
                }
                RightBounds = new Rectangle((int)Bounds.X + (sizeOfCenters / 2), (int)Bounds.Y - OffsetY, 64, 64);
            }
        }

        #endregion Public Methods
    }
}