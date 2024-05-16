using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;


namespace ArcardnoidContent.Components.GameScene.UI
{
    public class HeartUI : GameComponent
    {
        private ITexture heartTexture;
        private HearState state = HearState.Filling;
        private Dictionary<HearState, List<Rectangle>> _rectangles;
        private int _currentFrame = 0;
        private double _elapsedTime = 0;
        private double _frameSpeed = 0.05;
        private Rectangle _imageBounds;

        public HeartUI(int x, int y) : base( x, y, 58, 50)
        {
            _rectangles = new Dictionary<HearState, List<Rectangle>>();
        }

        public override void Load()
        {
            base.Load();
            heartTexture = GameServiceProvider.GetService<ITextureService>().Load("ui/heart");
            _rectangles.Add(HearState.Full, new List<Rectangle> { new Rectangle(260 * 4, 220 * 4, 260, 220) });
            _rectangles.Add(HearState.Empty, new List<Rectangle> { new Rectangle(0, 0, 260, 220) });
            _rectangles[HearState.Filling] = new List<Rectangle>();
            _rectangles[HearState.Emptying] = new List<Rectangle>();
            for (int y = 0; y < 1100; y = y + 220)
            {
                for (int x = 0; x < 1300; x = x + 260)
                {
                    _rectangles[HearState.Filling].Add(new Rectangle(x, y, 260, 220));
                }
            }
            for (int y = 880; y >= 0; y = y - 220)
            {
                for (int x = 1040; x >= 0; x = x - 260)
                {
                    _rectangles[HearState.Emptying].Add(new Rectangle(x, y, 260, 220));
                }
            }
        }
        public override void Draw()
        {
            base.Draw();
            heartTexture.DrawTexture(Bounds, _imageBounds, Color, Rotation, Point.Zero);
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            Rectangle[] rectangles = _rectangles[state].ToArray();
            _elapsedTime += delta;
            if (_elapsedTime >= _frameSpeed)
            {
                _elapsedTime = 0;
                _currentFrame++;
                if (_currentFrame >= rectangles.Length)
                {
                    _currentFrame = 0;
                    if (state == HearState.Filling)
                    {
                        state = HearState.Full;
                    }
                    if (state == HearState.Emptying)
                    {
                        state = HearState.Empty;
                    }
                }
            }
            _imageBounds = _rectangles[state][_currentFrame];
        }
    }
}
