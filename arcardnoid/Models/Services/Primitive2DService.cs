using arcardnoid.Models.Framework.Tools;
using arcardnoid.Models.Tools;
using ArcardnoidShared.Framework.Drawing;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace arcardnoid.Models.Services
{
    public class Primitive2DService : IPrimitives2D
    {
        #region Private Fields

        private SpriteBatch _spriteBatch;

        #endregion Private Fields

        #region Public Constructors

        public Primitive2DService(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        #endregion Public Constructors

        #region Public Methods

        public void DrawArc(Point center, float radius, int sides, float startingAngle, float radians, GameColor color)
        {
            _spriteBatch.DrawArc(center.ToVector2(), radius, sides, startingAngle, radians, color.ToXnaColor());
        }

        public void DrawArc(Point center, float radius, int sides, float startingAngle, float radians, GameColor color, float thickness)
        {
            _spriteBatch.DrawArc(center.ToVector2(), radius, sides, startingAngle, radians, color.ToXnaColor(), thickness);
        }

        public void DrawCircle(float x, float y, float radius, int sides, GameColor color)
        {
            _spriteBatch.DrawCircle(new Microsoft.Xna.Framework.Vector2(x, y), radius, sides, color.ToXnaColor());
        }

        public void DrawCircle(float x, float y, float radius, int sides, GameColor color, float thickness)
        {
            _spriteBatch.DrawCircle(new Microsoft.Xna.Framework.Vector2(x, y), radius, sides, color.ToXnaColor(), thickness);
        }

        public void DrawCircle(Point center, float radius, int sides, GameColor color)
        {
            _spriteBatch.DrawCircle(center.ToVector2(), radius, sides, color.ToXnaColor());
        }

        public void DrawCircle(Point center, float radius, int sides, GameColor color, float thickness)
        {
            _spriteBatch.DrawCircle(center.ToVector2(), radius, sides, color.ToXnaColor(), thickness);
        }

        public void DrawLine(float x1, float y1, float x2, float y2, GameColor color)
        {
            _spriteBatch.DrawLine(new Microsoft.Xna.Framework.Vector2(x1, y1), new Microsoft.Xna.Framework.Vector2(x2, y2), color.ToXnaColor());
        }

        public void DrawLine(float x1, float y1, float x2, float y2, GameColor color, float thickness)
        {
            _spriteBatch.DrawLine(new Microsoft.Xna.Framework.Vector2(x1, y1), new Microsoft.Xna.Framework.Vector2(x2, y2), color.ToXnaColor(), thickness);
        }

        public void DrawLine(Point point, float length, float angle, GameColor color)
        {
            _spriteBatch.DrawLine(point.ToVector2(), length, angle, color.ToXnaColor());
        }

        public void DrawLine(Point point, float length, float angle, GameColor color, float thickness)
        {
            _spriteBatch.DrawLine(point.ToVector2(), length, angle, color.ToXnaColor(), thickness);
        }

        public void DrawLine(Point point1, Point point2, GameColor color)
        {
            _spriteBatch.DrawLine(point1.ToVector2(), point2.ToVector2(), color.ToXnaColor());
        }

        public void DrawLine(Point point1, Point point2, GameColor color, float thickness)
        {
            _spriteBatch.DrawLine(point1.ToVector2(), point2.ToVector2(), color.ToXnaColor(), thickness);
        }

        public void DrawRectangle(Rectangle rect, GameColor color)
        {
            _spriteBatch.DrawRectangle(rect.ToXnaRectangle().ToRectangle(), color.ToXnaColor());
        }

        public void DrawRectangle(Rectangle rect, GameColor color, float thickness)
        {
            _spriteBatch.DrawRectangle(rect.ToXnaRectangle().ToRectangle(), color.ToXnaColor(), thickness);
        }

        public void DrawRectangle(Point location, Point size, GameColor color)
        {
            _spriteBatch.DrawRectangle(location.ToVector2(), size.ToVector2(), color.ToXnaColor());
        }

        public void DrawRectangle(Point location, Point size, GameColor color, float thickness)
        {
            _spriteBatch.DrawRectangle(location.ToVector2(), size.ToVector2(), color.ToXnaColor(), thickness);
        }

        public void FillRectangle(float x, float y, float w, float h, GameColor color)
        {
            _spriteBatch.FillRectangle(x, y, w, h, color.ToXnaColor());
        }

        public void FillRectangle(float x, float y, float w, float h, GameColor color, float angle)
        {
            Primitives2D.FillRectangle(_spriteBatch, x, y, w, h, color.ToXnaColor(), angle);
        }

        public void FillRectangle(Rectangle rect, GameColor color)
        {
            _spriteBatch.FillRectangle(rect.ToXnaRectangle().ToRectangle(), color.ToXnaColor());
        }

        public void FillRectangle(Rectangle rect, GameColor color, float angle)
        {
            _spriteBatch.FillRectangle(rect.ToXnaRectangle().ToRectangle(), color.ToXnaColor(), angle);
        }

        public void FillRectangle(Point location, Point size, GameColor color)
        {
            _spriteBatch.FillRectangle(location.ToVector2(), size.ToVector2(), color.ToXnaColor());
        }

        public void FillRectangle(Point location, Point size, GameColor color, float angle)
        {
            _spriteBatch.FillRectangle(location.ToVector2(), size.ToVector2(), color.ToXnaColor(), angle);
        }

        public void PutPixel(float x, float y, GameColor color)
        {
            _spriteBatch.PutPixel(x, y, color.ToXnaColor());
        }

        public void PutPixel(Point position, GameColor color)
        {
            _spriteBatch.PutPixel(position.ToVector2(), color.ToXnaColor());
        }

        #endregion Public Methods
    }
}