using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class HeartUI : GameComponent
    {
        #region Public Properties

        public HearState HeartState { get; set; } = HearState.Filling;

        #endregion Public Properties

        #region Private Fields

        private readonly double _frameSpeed = 0.05;
        private readonly Dictionary<HearState, List<Rectangle>> _rectangles;
        private int _currentFrame = 0;
        private double _elapsedTime = 0;
        private Rectangle _imageBounds = Rectangle.Empty;
        private ITexture? heartTexture;

        #endregion Private Fields

        #region Public Constructors

        public HeartUI(int x, int y) : base(x, y, 58, 50)
        {
            _rectangles = new Dictionary<HearState, List<Rectangle>>();
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            base.Draw();
            if (heartTexture == null) return;
            heartTexture.DrawTexture(RealBounds, _imageBounds, Color, Rotation, Point.Zero);
        }

        public override void Load()
        {
            base.Load();
            heartTexture = GameServiceProvider.GetService<ITextureService>().Load(TextureType.UI_HEART);
            _rectangles.Add(HearState.Full, new List<Rectangle> { new(260 * 4, 220 * 4, 260, 220) });
            _rectangles.Add(HearState.Empty, new List<Rectangle> { new(0, 0, 260, 220) });
            _rectangles[HearState.Filling] = new List<Rectangle>();
            _rectangles[HearState.Emptying] = new List<Rectangle>();
            for (int y = 0; y < 1100; y += 220)
            {
                for (int x = 0; x < 1300; x += 260)
                {
                    _rectangles[HearState.Filling].Add(new Rectangle(x, y, 260, 220));
                }
            }
            for (int y = 880; y >= 0; y -= 220)
            {
                for (int x = 1040; x >= 0; x -= 260)
                {
                    _rectangles[HearState.Emptying].Add(new Rectangle(x, y, 260, 220));
                }
            }
            _imageBounds = _rectangles[HeartState][_currentFrame];
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            Rectangle[] rectangles = _rectangles[HeartState].ToArray();
            _elapsedTime += delta;
            if (_elapsedTime >= _frameSpeed)
            {
                _elapsedTime = 0;
                _currentFrame++;
                if (_currentFrame >= rectangles.Length)
                {
                    _currentFrame = 0;
                    if (HeartState == HearState.Filling)
                    {
                        HeartState = HearState.Full;
                    }
                    if (HeartState == HearState.Emptying)
                    {
                        HeartState = HearState.Empty;
                    }
                }
            }
            _imageBounds = _rectangles[HeartState][_currentFrame];
        }

        #endregion Public Methods
    }
}