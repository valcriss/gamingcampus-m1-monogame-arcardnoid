using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace arcardnoid.Models.Framework.Components.UI
{
    public class Button : MouseInterractComponent
    {
        #region Private Properties

        private Rectangle CenterRectangle { get; set; }
        private Texture2D CurrentTexture { get; set; }
        private string HoverAsset { get; set; }
        private Texture2D HoverTexture { get; set; }
        private Rectangle LeftRectangle { get; set; }
        private string NormalAsset { get; set; }
        private Texture2D NormalTexture { get; set; }
        private string PressedAsset { get; set; }
        private Texture2D PressedTexture { get; set; }
        private List<Rectangle[]> Rectangles { get; set; } = new List<Rectangle[]>();
        private Rectangle RightRectangle { get; set; }
        private string Text { get; set; }
        private BitmapText TextComponent { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public Button(string name, string text, string normalAsset, string hoverAsset, string pressedAsset, Action onClick = null, int x = 0, int y = 0, int width = 0, int height = 0) : base(name, onClick, x, y, width, height)
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
                Game.SpriteBatch.Draw(CurrentTexture, rectangle[1], rectangle[0], Color);
            }
        }

        public override void Load()
        {
            base.Load();
            TextComponent = AddComponent(new BitmapText("buttontext", "fonts/band", Text, (int)Bounds.Width / 2, 22, TextHorizontalAlign.Center, TextVerticalAlign.Center));
            TextComponent.Color = Color.Black;
            NormalTexture = Game.Content.Load<Texture2D>(NormalAsset);
            HoverTexture = Game.Content.Load<Texture2D>(HoverAsset);
            PressedTexture = Game.Content.Load<Texture2D>(PressedAsset);
            CurrentTexture = NormalTexture;

            LeftRectangle = new Rectangle(0, 0, NormalTexture.Width / 3, NormalTexture.Height);
            CenterRectangle = new Rectangle(NormalTexture.Width / 3, 0, NormalTexture.Width / 3, NormalTexture.Height);
            RightRectangle = new Rectangle(NormalTexture.Width / 3 * 2, 0, NormalTexture.Width / 3, NormalTexture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (InterractState)
            {
                case MouseInterractState.Hover:
                    CurrentTexture = HoverTexture;
                    TextComponent.Position = new Vector2(TextComponent.Position.X, 22);
                    break;

                case MouseInterractState.Pressed:
                    CurrentTexture = PressedTexture;
                    TextComponent.Position = new Vector2(TextComponent.Position.X, 26);
                    break;

                default:
                    CurrentTexture = NormalTexture;
                    TextComponent.Position = new Vector2(TextComponent.Position.X, 22);
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