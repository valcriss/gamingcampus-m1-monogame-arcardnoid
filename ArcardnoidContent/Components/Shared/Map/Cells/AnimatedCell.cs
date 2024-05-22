using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.Shared.Map.Cells
{
    public class AnimatedCell : MapCell
    {
        #region Private Properties

        private static IRandom AnimRandom { get; set; } = GameServiceProvider.GetService<IRandomService>().GetRandom();

        #endregion Private Properties

        #region Private Fields

        private int _columns;
        private double _delay;
        private int _delayMax;
        private int _delayMin;
        private double _elapsedTime;
        private int _index;
        private double _pausedTime;
        private List<Rectangle> _rects = new List<Rectangle>();
        private int _rows;
        private double _speed;

        #endregion Private Fields

        #region Public Constructors

        public AnimatedCell(ITexture texture, int columns, int rows, double speed, int delayMin, int delayMax, int x, int y, int realX, int realY, int offsetX, int offsetY) : base(texture, x, y, realX, realY, offsetX, offsetY)
        {
            _columns = columns;
            _rows = rows;
            _speed = speed;
            _index = 0;
            _elapsedTime = 0;
            _delayMin = delayMin;
            _delayMax = delayMax;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            Texture2D.DrawTexture(DrawBounds, _rects.ElementAt(_index), Color, Rotation, Origin);
        }

        public override void Load()
        {
            base.Load();
            CalculateRects();
            _delay = (_delayMin == 0 && _delayMax == 0) ? 0 : AnimRandom.Next(_delayMin, _delayMax);
            Bounds = new Rectangle(Bounds.X, Bounds.Y, Texture2D.Width / _columns, Texture2D.Height / _rows);
            Origin = new Point(Bounds.Width / 2, Bounds.Height / 2);
            DrawOrigin = new Point(0, 0);
            UpdateRenderBounds();
        }

        public override void Update(float delta)
        {
            _elapsedTime += delta * 1000;
            if (_elapsedTime >= _speed)
            {
                _elapsedTime = 0;
                int nextIndex = _index + 1;
                if (nextIndex < _columns * _rows)
                {
                    _index = nextIndex;
                    _pausedTime = 0;
                }
                else
                {
                    if (_delay > 0 && _pausedTime < _delay)
                    {
                        _pausedTime += delta * 1000;
                    }
                    else
                    {
                        _index = 0;
                        _pausedTime = 0;
                        _delay = (_delayMin == 0 && _delayMax == 0) ? 0 : AnimRandom.Next(_delayMin, _delayMax);
                    }
                }
            }
            base.Update(delta);
        }

        #endregion Public Methods

        #region Private Methods

        private void CalculateRects()
        {
            _rects = new List<Rectangle>(_rows * _columns);
            int spriteWidth = Texture2D.Width / _columns;
            int spriteHeight = Texture2D.Height / _rows;

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    int rectX = j * spriteWidth;
                    int rectY = i * spriteHeight;
                    _rects.Add(new Rectangle(rectX, rectY, spriteWidth, spriteHeight));
                }
            }
        }

        #endregion Private Methods
    }
}