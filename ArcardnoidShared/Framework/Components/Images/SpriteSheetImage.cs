﻿using ArcardnoidShared.Framework.Drawing;

namespace ArcardnoidShared.Framework.Components.Images
{
    public class SpriteSheetImage : ImagePart
    {
        #region Private Fields

        private int _columns;
        private double _elapsedTime;
        private int _index;
        private List<Rectangle> _rects = new List<Rectangle>();
        private int _rows;
        private float _speed;

        #endregion Private Fields

        #region Public Constructors

        public SpriteSheetImage(string spriteAsset, int columns, int rows, float speed, int x, int y) : base(spriteAsset, x, y, default)
        {
            _columns = columns;
            _rows = rows;
            _speed = speed;
            _index = 0;
            _elapsedTime = 0;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            if (ImageTexture == null) return;
            CalculateRects();
            Bounds = new Rectangle(Bounds.X, Bounds.Y, ImageTexture.Width / _columns, ImageTexture.Height / _rows);
            Origin = new Point(Bounds.Width / 2, Bounds.Height / 2);
            DrawOrigin = new Point(0, 0);
            UpdateRenderBounds();
        }

        public override void Update(float delta)
        {
            if (ImageTexture == null) return;
            _elapsedTime += delta;
            if (_elapsedTime >= _speed)
            {
                _elapsedTime = 0;
                _index++;
                if (_index >= _columns * _rows)
                {
                    _index = 0;
                }
            }
            SetSourceRect(_rects[_index]);
            base.Update(delta);
        }

        #endregion Public Methods

        #region Private Methods

        private void CalculateRects()
        {
            if (ImageTexture == null) return;
            _rects = new List<Rectangle>(_rows * _columns);
            int spriteWidth = ImageTexture.Width / _columns;
            int spriteHeight = ImageTexture.Height / _rows;

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