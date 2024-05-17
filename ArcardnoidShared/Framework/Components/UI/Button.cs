﻿using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidShared.Framework.Components.UI
{
    public class Button : MouseInterractComponent
    {
        #region Private Properties

        private Rectangle CenterRectangle { get; set; }
        private ITexture CurrentTexture { get; set; }
        private string HoverAsset { get; set; }
        private ITexture HoverTexture { get; set; }
        private Rectangle LeftRectangle { get; set; }
        private string NormalAsset { get; set; }
        private ITexture NormalTexture { get; set; }
        private string PressedAsset { get; set; }
        private ITexture PressedTexture { get; set; }
        private List<Rectangle[]> Rectangles { get; set; } = new List<Rectangle[]>();
        private Rectangle RightRectangle { get; set; }
        private string Text { get; set; }
        private BitmapText TextComponent { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public Button(string text, string normalAsset, string hoverAsset, string pressedAsset, Action onClick = null, int x = 0, int y = 0, int width = 0, int height = 0) : base(onClick, x, y, width, height)
        {
            NormalAsset = normalAsset;
            HoverAsset = hoverAsset;
            PressedAsset = pressedAsset;
            Text = text;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            base.Draw();
            foreach (Rectangle[] rectangle in Rectangles)
            {
                CurrentTexture.DrawTexture(rectangle[1], rectangle[0], Color, 0, Point.Zero);
            }
        }

        public override void Load()
        {
            base.Load();
            TextComponent = AddGameComponent(new BitmapText("fonts/band", Text, (int)Bounds.Width / 2, 22, TextHorizontalAlign.Center, TextVerticalAlign.Center));
            TextComponent.Color = GameColor.Black;
            NormalTexture = GameServiceProvider.GetService<ITextureService>().Load(NormalAsset);
            HoverTexture = GameServiceProvider.GetService<ITextureService>().Load(HoverAsset);
            PressedTexture = GameServiceProvider.GetService<ITextureService>().Load(PressedAsset);
            CurrentTexture = NormalTexture;

            LeftRectangle = new Rectangle(0, 0, NormalTexture.Width / 3, NormalTexture.Height);
            CenterRectangle = new Rectangle(NormalTexture.Width / 3, 0, NormalTexture.Width / 3, NormalTexture.Height);
            RightRectangle = new Rectangle(NormalTexture.Width / 3 * 2, 0, NormalTexture.Width / 3, NormalTexture.Height);
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            switch (InterractState)
            {
                case MouseInterractState.Hover:
                    CurrentTexture = HoverTexture;
                    TextComponent.Position = new Point(TextComponent.Position.X, 22);
                    break;

                case MouseInterractState.Pressed:
                    CurrentTexture = PressedTexture;
                    TextComponent.Position = new Point(TextComponent.Position.X, 26);
                    break;

                default:
                    CurrentTexture = NormalTexture;
                    TextComponent.Position = new Point(TextComponent.Position.X, 22);
                    break;
            }

            int width = CurrentTexture.Width / 3;
            int height = CurrentTexture.Height;
            Rectangles = new List<Rectangle[]>
            {
                new Rectangle[] { LeftRectangle, new Rectangle((int)RealBounds.X, (int)RealBounds.Y, LeftRectangle.Width, (int)RealBounds.Height) },
            };

            for (int x = (int)RealBounds.X + width; x < RealBounds.X + RealBounds.Width - width; x += width)
            {
                Rectangles.Add(new Rectangle[] { CenterRectangle, new Rectangle(x, (int)RealBounds.Y, width, height) });
            }

            Rectangles.Add(new Rectangle[] { RightRectangle, new Rectangle((int)RealBounds.X + (int)RealBounds.Width - RightRectangle.Width, (int)RealBounds.Y, RightRectangle.Width, (int)RealBounds.Height) });
        }

        #endregion Public Methods
    }
}