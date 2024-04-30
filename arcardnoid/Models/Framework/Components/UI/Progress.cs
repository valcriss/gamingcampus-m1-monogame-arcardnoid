using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Scenes;
using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace arcardnoid.Models.Framework.Components.UI
{
    public class Progress : Component
    {
        #region Public Properties

        public string HoverAsset { get; set; }
        public string NormalAsset { get; set; }
        public string PressedAsset { get; set; }
        public float Value { get; set; } = 0;

        #endregion Public Properties

        #region Private Properties

        private int BarWidth { get; set; }
        private Texture2D CurrentLeftTexture { get; set; }
        private Texture2D CurrentRightTexture { get; set; }
        private Texture2D HoverTexture { get; set; }
        private bool IsPressed { get; set; } = false;
        private Rectangle LeftRectangle { get; set; }
        private Rectangle MiddleRectangle { get; set; }
        private Texture2D NormalTexture { get; set; }
        private Texture2D PressedTexture { get; set; }
        private Rectangle RightRectangle { get; set; }
        private BitmapText Text { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public Progress(string name, string normalAsset, string hoverAsset, string pressedAsset, float value, int x = 0, int y = 0, int width = 0) : base(name, x, y, (width + 2) * 64, 64)
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
            Game.SpriteBatch.Draw(CurrentLeftTexture, ScreenManager.Scale(new Rectangle((int)RealBounds.X, (int)RealBounds.Y, 64, 64)), LeftRectangle, Color);
            Primitives2D.FillRectangle(Game.SpriteBatch, ScreenManager.Scale(new Rectangle((int)RealBounds.X + 64, (int)RealBounds.Y + 6, (BarWidth * 64), 48)), Color.FromNonPremultiplied(220, 220, 220, Color.A));
            Primitives2D.FillRectangle(Game.SpriteBatch, ScreenManager.Scale(new Rectangle((int)RealBounds.X + 64, (int)RealBounds.Y + 6, (int)(BarWidth * Value * 64), 48)), Color.FromNonPremultiplied(187, 181, 82, Color.A));
            for (int i = 0; i < BarWidth; i++)
            {
                Game.SpriteBatch.Draw(CurrentLeftTexture, ScreenManager.Scale(new Rectangle((int)RealBounds.X + 64 + i * 64, (int)RealBounds.Y, 64, 64)), MiddleRectangle, Color);
            }
            Game.SpriteBatch.Draw(CurrentRightTexture, ScreenManager.Scale(new Rectangle((int)RealBounds.X + 64 + BarWidth * 64, (int)RealBounds.Y, 64, 64)), RightRectangle, Color);
        }

        public float GetValue()
        {
            return Value;
        }

        public override void Load()
        {
            base.Load();

            NormalTexture = Game.Content.Load<Texture2D>(NormalAsset);
            HoverTexture = Game.Content.Load<Texture2D>(HoverAsset);
            PressedTexture = Game.Content.Load<Texture2D>(PressedAsset);
            CurrentLeftTexture = NormalTexture;
            CurrentRightTexture = NormalTexture;
            LeftRectangle = new Rectangle(0, 0, 64, 64);
            MiddleRectangle = new Rectangle(64, 0, 64, 64);
            RightRectangle = new Rectangle(128, 0, 64, 64);
            Text = AddComponent(new BitmapText(Name + "Text", "fonts/band", GetValueAsString(), 64 + ((BarWidth / 2) * 64), 28, TextHorizontalAlign.Center, TextVerticalAlign.Center, Color.Black));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MouseState state = Mouse.GetState();
            Point mousePoint = ScreenManager.UIScale(state.Position);
            Rectangle LeftRealBounds = new Rectangle((int)RealBounds.X, (int)RealBounds.Y, 64, 64);
            Rectangle RightRealBounds = new Rectangle((int)RealBounds.X + 64 + BarWidth * 64, (int)RealBounds.Y, 64, 64);
            if (LeftRealBounds.Contains(mousePoint))
            {
                CurrentLeftTexture = HoverTexture;
                CurrentRightTexture = NormalTexture;
                if (state.LeftButton == ButtonState.Pressed)
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
                if (state.LeftButton == ButtonState.Pressed)
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