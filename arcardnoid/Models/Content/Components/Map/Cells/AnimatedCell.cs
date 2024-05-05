using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System.Collections.Generic;
using System.Linq;

namespace arcardnoid.Models.Content.Components.Map.Cells
{
    public class AnimatedCell : MapCell
    {
        #region Private Properties

        private static FastRandom AnimRandom { get; set; } = new FastRandom();

        #endregion Private Properties

        #region Private Fields

        private int _columns;
        private double _delay;
        private int _delayMax;
        private int _delayMin;
        private double _elapsedTime;
        private int _index;
        private double _pausedTime;
        private List<Rectangle> _rects;
        private int _rows;
        private double _speed;

        #endregion Private Fields

        #region Public Constructors

        public AnimatedCell(string name, Texture2D texture, int columns, int rows, double speed, int delayMin, int delayMax, int x, int y, int realX, int realY, int offsetX, int offsetY) : base(name, texture, x, y, realX, realY, offsetX, offsetY)
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
            Game.SpriteBatch.Draw(Texture2D, ScreenManager.Scale(DrawBounds).ToRectangle(), _rects.ElementAt(_index), Color, MathHelper.ToRadians(Rotation), Origin, SpriteEffects.None, 0);
        }

        public override void Load()
        {
            base.Load();
            CalculateRects();
            _delay = (_delayMin == 0 && _delayMax == 0) ? 0 : AnimRandom.Next(_delayMin, _delayMax);
            Bounds = new RectangleF(Bounds.X, Bounds.Y, Texture2D.Width / _columns, Texture2D.Height / _rows);
            Origin = new Vector2(Bounds.Width / 2, Bounds.Height / 2);
            DrawOrigin = new Vector2(0, 0);
            UpdateRenderBounds();
        }

        public override void Update(GameTime gameTime)
        {
            double ms = gameTime.ElapsedGameTime.TotalMilliseconds;
            _elapsedTime += ms;
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
                        _pausedTime += ms;
                    }
                    else
                    {
                        _index = 0;
                        _pausedTime = 0;
                        _delay = (_delayMin == 0 && _delayMax == 0) ? 0 : AnimRandom.Next(_delayMin, _delayMax);
                    }
                }
            }
            base.Update(gameTime);
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