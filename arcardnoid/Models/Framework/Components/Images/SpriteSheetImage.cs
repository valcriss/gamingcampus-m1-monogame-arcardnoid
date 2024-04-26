﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System.Collections.Generic;
using System.Linq;

namespace arcardnoid.Models.Framework.Components.Images
{
    public class SpriteSheetImage : Image
    {
        #region Private Fields

        private int _columns;
        private double _elapsedTime;
        private int _index;
        private List<Rectangle> _rects;
        private int _rows;
        private double _speed;

        #endregion Private Fields

        #region Public Constructors

        public SpriteSheetImage(string spriteAsset, int columns, int rows, double speed, int x, int y) : base(spriteAsset, x, y)
        {
            _columns = columns;
            _rows = rows;
            _speed = speed;
            _index = 0;
            _elapsedTime = 0;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            Game.SpriteBatch.Draw(Sprite, DrawBounds.ToRectangle(), _rects.ElementAt(_index), Color, MathHelper.ToRadians(Rotation), Origin, SpriteEffects.None, 0);
        }

        public override void Load()
        {
            base.Load();
            CalculateRects();
            this.Bounds = new RectangleF(Bounds.X, Bounds.Y, Sprite.Width / _columns, Sprite.Height / _rows);
            this.Origin = new Vector2(Bounds.Width / 2, Bounds.Height / 2);
            this.DrawOrigin = new Vector2(0, 0);
            this.UpdateRenderBounds();
        }

        public override void Update(GameTime gameTime)
        {
            _elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_elapsedTime >= _speed)
            {
                _elapsedTime = 0;
                _index++;
                if (_index >= _columns * _rows)
                {
                    _index = 0;
                }
            }
            base.Update(gameTime);
        }

        #endregion Public Methods

        #region Private Methods

        private void CalculateRects()
        {
            _rects = new List<Rectangle>(_rows * _columns);
            int spriteWidth = Sprite.Width / _columns;
            int spriteHeight = Sprite.Height / _rows;

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