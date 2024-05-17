using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidShared.Framework.Components.UI
{
    public class Input : GameComponent
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
        private ITexture _inputTexture;
        private double _lastCursorTime = 0;
        private DateTime _lastKeyTime = DateTime.Now;
        private string _text;

        #endregion Private Fields

        #region Public Constructors

        public Input(string inputAsset, string value = "", int x = 0, int y = 0, int width = 0) : base(x, y, width * 64, 128)
        {
            InputWidth = width;
            InputAsset = inputAsset;
            _text = value;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            _inputTexture.DrawTexture(new Rectangle((int)RealBounds.X, (int)RealBounds.Y, 64, 128), LeftRectangle, Color, 0, Point.Zero);
            for (int i = 0; i < InputWidth; i++)
            {
                _inputTexture.DrawTexture(new Rectangle((int)RealBounds.X + 64 + i * 64, (int)RealBounds.Y, 64, 128), CenterRectangle, Color, 0, Point.Zero);
            }
            _inputTexture.DrawTexture(new Rectangle((int)RealBounds.X + 64 + InputWidth * 64, (int)RealBounds.Y, 64, 128), RightRectangle, Color, 0, Point.Zero);
            base.Draw();
        }

        public string GetValue()
        {
            return _text;
        }

        public override void Load()
        {
            base.Load();
            _inputTexture = GameServiceProvider.GetService<ITextureService>().Load(InputAsset);
            LeftRectangle = new Rectangle(0, 0, 64, 128);
            CenterRectangle = new Rectangle(64, 0, 64, 128);
            RightRectangle = new Rectangle(128, 0, 64, 128);
            Text = AddGameComponent(new BitmapText("fonts/band", _text, 64 + ((InputWidth / 2) * 64), 50, TextHorizontalAlign.Center, TextVerticalAlign.Center, GameColor.Black));
        }

        public void SetValue(string value)
        {
            _text = value;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            IMouseService mouseService = GameServiceProvider.GetService<IMouseService>();

            Point mousePoint = ScreenManager.UIScale(mouseService.GetMousePosition());
            if (IsDisabled)
            {
                IsHovered = false;
                IsFocused = false;
                IsPressed = false;
                return;
            }
            Rectangle bounds = new Rectangle((int)RealBounds.X, (int)RealBounds.Y, 64 + InputWidth * 64, 128);
            IsHovered = bounds.Contains(mousePoint);
            if (IsHovered)
            {
                if (mouseService.IsMouseLeftButtonPressed())
                {
                    IsPressed = true;
                    IsFocused = true;
                    _lastCursorTime = 0;
                }
            }
            else if (mouseService.IsMouseLeftButtonPressed())
            {
                IsPressed = false;
                IsFocused = false;
                _cursorVisible = false;
                _lastCursorTime = 0;
            }

            if (IsFocused)
            {
                _lastCursorTime = _lastCursorTime + delta;
                if (_lastCursorTime > _cursorBlinkTime)
                {
                    _lastCursorTime = 0;
                    _cursorVisible = !_cursorVisible;
                }

                IKeyboardService Keyboard = GameServiceProvider.GetService<IKeyboardService>();

                if ((Keyboard.IsKeyDown("NumPad0") || Keyboard.IsKeyDown("D0")) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "0";
                }
                if ((Keyboard.IsKeyDown("NumPad1") || Keyboard.IsKeyDown("D1")) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "1";
                }
                if ((Keyboard.IsKeyDown("NumPad2") || Keyboard.IsKeyDown("D2")) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "2";
                }
                if ((Keyboard.IsKeyDown("NumPad3") || Keyboard.IsKeyDown("D3")) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "3";
                }
                if ((Keyboard.IsKeyDown("NumPad4") || Keyboard.IsKeyDown("D4")) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "4";
                }
                if ((Keyboard.IsKeyDown("NumPad5") || Keyboard.IsKeyDown("D5")) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "5";
                }
                if ((Keyboard.IsKeyDown("NumPad6") || Keyboard.IsKeyDown("D6")) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "6";
                }
                if ((Keyboard.IsKeyDown("NumPad7") || Keyboard.IsKeyDown("D7")) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "7";
                }
                if ((Keyboard.IsKeyDown("NumPad8") || Keyboard.IsKeyDown("D8")) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "8";
                }
                if ((Keyboard.IsKeyDown("NumPad9") || Keyboard.IsKeyDown("D9")) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    _text = _text + "9";
                }
                if ((Keyboard.IsKeyDown("Back")) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    _lastKeyTime = DateTime.Now;
                    if (_text.Length > 0)
                        _text = _text.Substring(0, _text.Length - 1);
                }
                if ((Keyboard.IsKeyDown("Escape") || Keyboard.IsKeyDown("Enter") || Keyboard.IsKeyDown("Tab")) && DateTime.Now.Subtract(_lastKeyTime).TotalMilliseconds > 100)
                {
                    IsFocused = false;
                    _cursorVisible = false;
                }
                Text.Color = GameColor.Black;
            }
            else
            {
                _cursorVisible = false;
                Text.Color = new GameColor(70, 70, 70, 255);
            }
            Text.SetText(_text + (_cursorVisible && IsFocused ? "|" : ""));
        }

        #endregion Public Methods
    }
}