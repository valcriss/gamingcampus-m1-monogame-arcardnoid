using arcardnoid.Models.Framework.Components.Texts;
using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace arcardnoid.Models.Framework.Components.UI
{
    public class Input : Component
    {
        #region Public Properties

        public Rectangle CenterRectangle { get; set; }
        public string InputAsset { get; set; }
        public int InputWidth { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsFocused { get; set; }
        public bool IsHovered { get; set; }
        public bool IsPressed { get; set; }
        public Rectangle LeftRectangle { get; set; }
        public Rectangle RightRectangle { get; set; }

        #endregion Public Properties

        #region Private Properties

        private BitmapText Text { get; set; }

        #endregion Private Properties

        #region Private Fields

        private double _cursorBlinkTime = 0.5;
        private bool _cursorVisible = true;
        private Texture2D _inputTexture;
        private double _lastCursorTime = 0;
        private DateTime _lastKeyTime = DateTime.Now;
        private string _text;

        #endregion Private Fields

        #region Public Constructors

        public Input(string name, string inputAsset, int x = 0, int y = 0, int width = 0) : base(name, x, y, width * 64, 128)
        {
            Random random = new Random();
            InputWidth = width;
            InputAsset = inputAsset;
            _text = random.Next(100000, 999999).ToString();
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            Game.SpriteBatch.Draw(_inputTexture, new Rectangle((int)RealBounds.X, (int)RealBounds.Y, 64, 128), LeftRectangle, Color);
            for (int i = 0; i < InputWidth; i++)
            {
                Game.SpriteBatch.Draw(_inputTexture, new Rectangle((int)RealBounds.X + 64 + i * 64, (int)RealBounds.Y, 64, 128), CenterRectangle, Color);
            }
            Game.SpriteBatch.Draw(_inputTexture, new Rectangle((int)RealBounds.X + 64 + InputWidth * 64, (int)RealBounds.Y, 64, 128), RightRectangle, Color);
            base.Draw();
        }

        public int GetValue()
        {
            return int.Parse(_text);
        }

        public override void Load()
        {
            base.Load();
            _inputTexture = Game.Content.Load<Texture2D>(InputAsset);
            LeftRectangle = new Rectangle(0, 0, 64, 128);
            CenterRectangle = new Rectangle(64, 0, 64, 128);
            RightRectangle = new Rectangle(128, 0, 64, 128);
            Text = AddComponent(new BitmapText(Name + "Text", "fonts/band", _text, 64 + ((InputWidth / 2) * 64), 50, TextHorizontalAlign.Center, TextVerticalAlign.Center, Color.Black));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MouseState mouseState = Mouse.GetState();
            if (IsDisabled)
            {
                IsHovered = false;
                IsFocused = false;
                IsPressed = false;
                return;
            }
            Rectangle bounds = new Rectangle((int)RealBounds.X, (int)RealBounds.Y, 64 + InputWidth * 64, 128);
            IsHovered = bounds.Contains(mouseState.Position);
            if (IsHovered)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    IsPressed = true;
                    IsFocused = true;
                    _lastCursorTime = 0;
                }
            }
            else if (mouseState.LeftButton == ButtonState.Pressed)
            {
                IsPressed = false;
                IsFocused = false;
                _cursorVisible = false;
                _lastCursorTime = 0;
            }

            if (IsFocused)
            {
                _lastCursorTime = _lastCursorTime + gameTime.ElapsedGameTime.TotalSeconds;
                if (_lastCursorTime > _cursorBlinkTime)
                {
                    _lastCursorTime = 0;
                    _cursorVisible = !_cursorVisible;
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.NumPad0) || Keyboard.GetState().IsKeyDown(Keys.D0)) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "0";
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.NumPad1) || Keyboard.GetState().IsKeyDown(Keys.D1)) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "1";
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.NumPad2) || Keyboard.GetState().IsKeyDown(Keys.D2)) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "2";
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.NumPad3) || Keyboard.GetState().IsKeyDown(Keys.D3)) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "3";
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.NumPad4) || Keyboard.GetState().IsKeyDown(Keys.D4)) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "4";
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.NumPad5) || Keyboard.GetState().IsKeyDown(Keys.D5)) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "5";
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.NumPad6) || Keyboard.GetState().IsKeyDown(Keys.D6)) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "6";
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.NumPad7) || Keyboard.GetState().IsKeyDown(Keys.D7)) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "7";
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.NumPad8) || Keyboard.GetState().IsKeyDown(Keys.D8)) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "8";
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.NumPad9) || Keyboard.GetState().IsKeyDown(Keys.D9)) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "9";
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.Back)) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    if (_text.Length > 0)
                        _text = _text.Substring(0, _text.Length - 1);
                }
                if ((Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Tab)) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    IsFocused = false;
                    _cursorVisible = false;
                }
                Text.Color = Color.Black;
            }
            else
            {
                _cursorVisible = false;
                Text.Color = Color.FromNonPremultiplied(70, 70, 70, 255);
            }
            Text.SetText(_text + (_cursorVisible && IsFocused ? "|" : ""));
        }

        #endregion Public Methods
    }
}