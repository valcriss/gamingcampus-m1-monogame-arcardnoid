using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Scenes;
using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace arcardnoid.Models.Content.Components.UI
{
    public class AnimatedTitleBand : Component
    {
        #region Private Properties

        private string BandAsset { get; set; }
        private Texture2D BandTexture { get; set; }
        private BitmapText BitmapText { get; set; }
        private string CurrentText { get; set; }
        private double ElapsedTime { get; set; }
        private string FontAsset { get; set; }
        private List<Rectangle> InsideBounds { get; set; } = new List<Rectangle>();
        private Rectangle LeftBounds { get; set; }
        private Rectangle RightBounds { get; set; }
        private double Speed { get; set; }
        private string Text { get; set; }
        private Color TextColor { get; set; }

        #endregion Private Properties

        #region Private Fields

        private const int OffsetY = 24;

        #endregion Private Fields

        #region Public Constructors

        public AnimatedTitleBand(string bandAsset, string fontAsset, string text, double speed, int x, int y, Color textColor) : base($"AnimatedTitleBand-{x}-{y}", x, y)
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
            // Draw left
            Game.SpriteBatch.Draw(BandTexture, ScreenManager.Scale(LeftBounds), new Rectangle(0, 0, 64, 64), Color);
            // Draw inside
            foreach (var insideBound in InsideBounds)
            {
                Game.SpriteBatch.Draw(BandTexture, ScreenManager.Scale(insideBound), new Rectangle(64, 0, 64, 64), Color);
            }

            // Draw right
            Game.SpriteBatch.Draw(BandTexture, ScreenManager.Scale(RightBounds), new Rectangle(128, 0, 64, 64), Color);
        }

        public override void Load()
        {
            base.Load();
            BandTexture = Game.Content.Load<Texture2D>(BandAsset);
            BitmapText = AddComponent(new BitmapText("AnimatedTitleBand-title", FontAsset, CurrentText, 0, 0, TextHorizontalAlign.Center, TextVerticalAlign.Center));
            BitmapText.Color = TextColor;
            ElapsedTime = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (CurrentText.Length < Text.Length)
            {
                ElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
                if (ElapsedTime >= Speed)
                {
                    int index = CurrentText.Length;
                    CurrentText += Text[index];
                    BitmapText.SetText(CurrentText);
                    ElapsedTime = 0;
                }
                Size2 size = BitmapText.MeasureString();
                int numberOfCenters = (int)Math.Ceiling(size.Width / 64);
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