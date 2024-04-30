using arcardnoid.Models.Framework.Scenes;
using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;

namespace arcardnoid.Models.Framework.Components.Texts
{
    public class BitmapText : Component
    {
        #region Private Fields

        private BitmapFont _bitmapFont;
        private string _fontAsset;
        private TextHorizontalAlign _horizontalAlign;
        private string _text;
        private TextVerticalAlign _verticalAlign;

        #endregion Private Fields

        #region Public Constructors

        public BitmapText(string name, string fontAsset, string text, int x, int y, TextHorizontalAlign horizontalAlign = TextHorizontalAlign.Left, TextVerticalAlign verticalAlign = TextVerticalAlign.Top, Color? color = null) : base(name, x, y)
        {
            _fontAsset = fontAsset;
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
            Vector2 position = RealPosition;
            Size2 size = _bitmapFont.MeasureString(_text);
            if (_horizontalAlign == TextHorizontalAlign.Center)
            {
                position.X = position.X - (size.Width / 2);
            }
            else if (_horizontalAlign == TextHorizontalAlign.Right)
            {
                position.X -= size.Width;
            }
            if (_verticalAlign == TextVerticalAlign.Center)
            {
                position.Y -= size.Height / 2;
            }
            else if (_verticalAlign == TextVerticalAlign.Bottom)
            {
                position.Y -= size.Height;
            }

            Game.SpriteBatch.DrawString(_bitmapFont, _text, ScreenManager.Scale(position), Color,0,Vector2.Zero, ScreenManager.Scale(),Microsoft.Xna.Framework.Graphics.SpriteEffects.None,0);
            base.Draw();
        }

        public override void Load()
        {
            _bitmapFont = Game.Content.Load<BitmapFont>(_fontAsset);
            base.Load();
        }

        public Size2 MeasureString()
        {
            return _bitmapFont.MeasureString(_text);
        }

        public void SetText(string text)
        {
            _text = text;
        }

        #endregion Public Methods
    }
}