﻿using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidShared.Framework.Components.Text
{
    public class BitmapText : GameComponent
    {
        #region Private Fields

        private readonly BitmapFontType _fontType;
        private readonly TextHorizontalAlign _horizontalAlign;
        private readonly TextVerticalAlign _verticalAlign;
        private IBitmapFont? _bitmapFont;
        private string _text;

        #endregion Private Fields

        #region Public Constructors

        public BitmapText(BitmapFontType fontType, string text, int x, int y, TextHorizontalAlign horizontalAlign = TextHorizontalAlign.Left, TextVerticalAlign verticalAlign = TextVerticalAlign.Top, GameColor? color = null) : base(x, y)
        {
            _fontType = fontType;
            _text = text;
            _horizontalAlign = horizontalAlign;
            _verticalAlign = verticalAlign;
            if (color != null)
            {
                Color = color.Value;
            }
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            if (_bitmapFont == null) return;

            Point position = RealBounds.Position;
            Point size = _bitmapFont.MeasureString(_text);
            if (_horizontalAlign == TextHorizontalAlign.Center)
            {
                position.X -= (size.X / 2);
            }
            else if (_horizontalAlign == TextHorizontalAlign.Right)
            {
                position.X -= size.X;
            }
            if (_verticalAlign == TextVerticalAlign.Center)
            {
                position.Y -= size.Y / 2;
            }
            else if (_verticalAlign == TextVerticalAlign.Bottom)
            {
                position.Y -= size.Y;
            }

            _bitmapFont.DrawString(_text, ScreenManager.Scale(position), Color, Rotation, ScreenManager.Scale());

            base.Draw();
        }

        public override void Load()
        {
            _bitmapFont = GameServiceProvider.GetService<IBitmapFontService>().Load(_fontType);
            base.Load();
        }

        public Point MeasureString()
        {
            if (_bitmapFont == null)
            {
                throw new InvalidOperationException("BitmapFont is not loaded");
            }
            return _bitmapFont.MeasureString(_text);
        }

        public void SetText(string text)
        {
            _text = text;
        }

        #endregion Public Methods
    }
}