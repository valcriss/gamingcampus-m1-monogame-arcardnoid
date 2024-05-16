using arcardnoid.Models.Framework.Tools;
using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArcardnoidShared.Framework.Components.UI
{
    public class Progress : GameComponent
    {
        public string HoverAsset { get; set; }
        public string NormalAsset { get; set; }
        public string PressedAsset { get; set; }
        public float Value { get; set; } = 0;

        private int BarWidth { get; set; }
        private ITexture CurrentLeftTexture { get; set; }
        private ITexture CurrentRightTexture { get; set; }
        private ITexture HoverTexture { get; set; }
        private bool IsPressed { get; set; } = false;
        private Rectangle LeftRectangle { get; set; }
        private Rectangle MiddleRectangle { get; set; }
        private ITexture NormalTexture { get; set; }
        private ITexture PressedTexture { get; set; }
        private Rectangle RightRectangle { get; set; }
        private BitmapText Text { get; set; }
        private IPrimitives2D Primitives2D { get; set; }

        public Progress(string normalAsset, string hoverAsset, string pressedAsset, float value, int x = 0, int y = 0, int width = 0) : base(x, y, (width + 2) * 64, 64)
        {
            NormalAsset = normalAsset;
            HoverAsset = hoverAsset;
            PressedAsset = pressedAsset;
            Value = value;
            BarWidth = width;
        }

        public override void Load()
        {
            base.Load();
            NormalTexture =GameServiceProvider.GetService<ITextureService>().Load(NormalAsset);
            HoverTexture = GameServiceProvider.GetService<ITextureService>().Load(HoverAsset);
            PressedTexture = GameServiceProvider.GetService<ITextureService>().Load(PressedAsset);
            CurrentLeftTexture = NormalTexture;
            CurrentRightTexture = NormalTexture;
            LeftRectangle = new Rectangle(0, 0, 64, 64);
            MiddleRectangle = new Rectangle(64, 0, 64, 64);
            RightRectangle = new Rectangle(128, 0, 64, 64);
            Text = AddGameComponent(new BitmapText("fonts/band", GetValueAsString(), 64 + ((BarWidth / 2) * 64), 28, TextHorizontalAlign.Center, TextVerticalAlign.Center, GameColor.Black));
            Primitives2D = GameServiceProvider.GetService<IPrimitives2D>();
        }

        public override void Draw()
        {
            base.Draw();
            CurrentLeftTexture.DrawTexture(new Rectangle((int)RealBounds.X, (int)RealBounds.Y, 64, 64), LeftRectangle, Color, 0, Point.Zero);
            Primitives2D.FillRectangle(new Rectangle((int)RealBounds.X + 64, (int)RealBounds.Y + 6, (BarWidth * 64), 48), new GameColor(220, 220, 220, Color.A));
            Primitives2D.FillRectangle(new Rectangle((int)RealBounds.X + 64, (int)RealBounds.Y + 6, (int)(BarWidth * Value * 64), 48), new GameColor(187, 181, 82, Color.A));
            for (int i = 0; i < BarWidth; i++)
            {
                CurrentLeftTexture.DrawTexture(new Rectangle((int)RealBounds.X + 64 + i * 64, (int)RealBounds.Y, 64, 64), MiddleRectangle, Color, 0, Point.Zero);
            }
            CurrentRightTexture.DrawTexture(new Rectangle((int)RealBounds.X + 64 + BarWidth * 64, (int)RealBounds.Y, 64, 64), RightRectangle, Color, 0, Point.Zero);
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            IMouseService mouseService = GameServiceProvider.GetService<IMouseService>();
            mouseService.Update();
            Point mousePoint = ScreenManager.UIScale(mouseService.GetMousePosition());
            Rectangle LeftRealBounds = new Rectangle((int)RealBounds.X, (int)RealBounds.Y, 64, 64);
            Rectangle RightRealBounds = new Rectangle((int)RealBounds.X + 64 + BarWidth * 64, (int)RealBounds.Y, 64, 64);
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

        private string GetValueAsString()
        {
            return (Value * 100).ToString("0") + "%";
        }
    }
}
