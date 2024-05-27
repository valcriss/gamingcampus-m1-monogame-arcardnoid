using arcardnoid.Models.Framework.Tools;
using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidShared.Framework.Components.UI
{
    public class Progress : GameComponent
    {
        #region Public Properties

        public TextureType HoverAsset { get; set; }
        public TextureType NormalAsset { get; set; }
        public TextureType PressedAsset { get; set; }
        public float Value { get; set; } = 0;

        #endregion Public Properties

        #region Private Properties

        private int BarWidth { get; set; }
        private ITexture? CurrentLeftTexture { get; set; }
        private ITexture? CurrentRightTexture { get; set; }
        private ITexture? HoverTexture { get; set; }
        private bool IsPressed { get; set; } = false;
        private Rectangle LeftRectangle { get; set; } = Rectangle.Empty;
        private Rectangle MiddleRectangle { get; set; } = Rectangle.Empty;
        private ITexture? NormalTexture { get; set; }
        private ITexture? PressedTexture { get; set; }
        private IPrimitives2D? Primitives2D { get; set; }
        private Rectangle RightRectangle { get; set; } = Rectangle.Empty;
        private BitmapText? Text { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public Progress(TextureType normalAsset, TextureType hoverAsset, TextureType pressedAsset, float value, int x = 0, int y = 0, int width = 0) : base(x, y, (width + 2) * 64, 64)
        {
            NormalAsset = normalAsset;
            HoverAsset = hoverAsset;
            PressedAsset = pressedAsset;
            Value = value;
            BarWidth = width;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            base.Draw();
            CurrentLeftTexture?.DrawTexture(new Rectangle((int)RealBounds.X, (int)RealBounds.Y, 64, 64), LeftRectangle, Color, 0, Point.Zero);
            Primitives2D?.FillRectangle(new Rectangle((int)RealBounds.X + 64, (int)RealBounds.Y + 6, (BarWidth * 64), 48), new GameColor(220, 220, 220, Color.A));
            Primitives2D?.FillRectangle(new Rectangle((int)RealBounds.X + 64, (int)RealBounds.Y + 6, (int)(BarWidth * Value * 64), 48), new GameColor(187, 181, 82, Color.A));
            for (int i = 0; i < BarWidth; i++)
            {
                CurrentLeftTexture?.DrawTexture(new Rectangle((int)RealBounds.X + 64 + i * 64, (int)RealBounds.Y, 64, 64), MiddleRectangle, Color, 0, Point.Zero);
            }
            CurrentRightTexture?.DrawTexture(new Rectangle((int)RealBounds.X + 64 + BarWidth * 64, (int)RealBounds.Y, 64, 64), RightRectangle, Color, 0, Point.Zero);
        }

        public override void Load()
        {
            base.Load();
            NormalTexture = GameServiceProvider.GetService<ITextureService>().Load(NormalAsset);
            HoverTexture = GameServiceProvider.GetService<ITextureService>().Load(HoverAsset);
            PressedTexture = GameServiceProvider.GetService<ITextureService>().Load(PressedAsset);
            CurrentLeftTexture = NormalTexture;
            CurrentRightTexture = NormalTexture;
            LeftRectangle = new Rectangle(0, 0, 64, 64);
            MiddleRectangle = new Rectangle(64, 0, 64, 64);
            RightRectangle = new Rectangle(128, 0, 64, 64);
            Text = AddGameComponent(new BitmapText(BitmapFontType.Default, GetValueAsString(), 64 + ((BarWidth / 2) * 64), 28, TextHorizontalAlign.Center, TextVerticalAlign.Center, GameColor.Black));
            Primitives2D = GameServiceProvider.GetService<IPrimitives2D>();
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            if (Text == null) return;
            IMouseService mouseService = GameServiceProvider.GetService<IMouseService>();

            Point mousePoint = ScreenManager.UIScale(mouseService.GetMousePosition());
            Rectangle LeftRealBounds = new((int)RealBounds.X, (int)RealBounds.Y, 64, 64);
            Rectangle RightRealBounds = new((int)RealBounds.X + 64 + BarWidth * 64, (int)RealBounds.Y, 64, 64);
            if (LeftRealBounds.Contains(mousePoint))
            {
                CurrentLeftTexture = HoverTexture;
                CurrentRightTexture = NormalTexture;
                if (mouseService.IsMouseLeftButtonPressed())
                {
                    CurrentLeftTexture = PressedTexture;
                    IsPressed = true;
                }
                else if (IsPressed)
                {
                    Value -= 0.05f;
                    Value = Math.Max(0, Value);
                    Text.SetText(GetValueAsString());
                    IsPressed = false;
                }
            }
            else if (RightRealBounds.Contains(mousePoint))
            {
                CurrentRightTexture = HoverTexture;
                CurrentLeftTexture = NormalTexture;
                if (mouseService.IsMouseLeftButtonPressed())
                {
                    CurrentRightTexture = PressedTexture;
                    IsPressed = true;
                }
                else if (IsPressed)
                {
                    Value += 0.05f;
                    Value = Math.Min(1, Value);
                    Text.SetText(GetValueAsString());
                    IsPressed = false;
                }
            }
            else
            {
                CurrentLeftTexture = NormalTexture;
                CurrentRightTexture = NormalTexture;
                IsPressed = false;
            }
        }

        #endregion Public Methods

        #region Private Methods

        private string GetValueAsString()
        {
            return (Value * 100).ToString("0") + "%";
        }

        #endregion Private Methods
    }
}