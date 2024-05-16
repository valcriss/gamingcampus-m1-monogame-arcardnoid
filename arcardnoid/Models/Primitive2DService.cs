using arcardnoid.Models.Framework.Tools;
using ArcardnoidShared.Framework.Drawing;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace arcardnoid.Models
{
    public class Primitive2DService : IPrimitives2D
    {

        private SpriteBatch _spriteBatch;
        public Primitive2DService(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void DrawArc(Point center, float radius, int sides, float startingAngle, float radians, GameColor color)
        {
            Primitives2D.DrawArc(_spriteBatch, center.ToVector2(), radius, sides, startingAngle, radians, color.ToXnaColor());
        }

        public void DrawArc(Point center, float radius, int sides, float startingAngle, float radians, GameColor color, float thickness)
        {
            Primitives2D.DrawArc(_spriteBatch, center.ToVector2(), radius, sides, startingAngle, radians, color.ToXnaColor(), thickness);
        }

        public void DrawCircle(float x, float y, float radius, int sides, GameColor color)
        {
            Primitives2D.DrawCircle(_spriteBatch, new Microsoft.Xna.Framework.Vector2(x, y), radius, sides, color.ToXnaColor());
        }

        public void DrawCircle(float x, float y, float radius, int sides, GameColor color, float thickness)
        {
            Primitives2D.DrawCircle(_spriteBatch, new Microsoft.Xna.Framework.Vector2(x, y), radius, sides, color.ToXnaColor(), thickness);
        }

        public void DrawCircle(Point center, float radius, int sides, GameColor color)
        {
            Primitives2D.DrawCircle(_spriteBatch, center.ToVector2(), radius, sides, color.ToXnaColor());
        }

        public void DrawCircle(Point center, float radius, int sides, GameColor color, float thickness)
        {
            Primitives2D.DrawCircle(_spriteBatch, center.ToVector2(), radius, sides, color.ToXnaColor(), thickness);
        }

        public void DrawLine(float x1, float y1, float x2, float y2, GameColor color)
        {
            Primitives2D.DrawLine(_spriteBatch, new Microsoft.Xna.Framework.Vector2(x1, y1), new Microsoft.Xna.Framework.Vector2(x2, y2), color.ToXnaColor());
        }

        public void DrawLine(float x1, float y1, float x2, float y2, GameColor color, float thickness)
        {
            Primitives2D.DrawLine(_spriteBatch, new Microsoft.Xna.Framework.Vector2(x1, y1), new Microsoft.Xna.Framework.Vector2(x2, y2), color.ToXnaColor(), thickness);
        }

        public void DrawLine(Point point, float length, float angle, GameColor color)
        {
            Primitives2D.DrawLine(_spriteBatch, point.ToVector2(), length, angle, color.ToXnaColor());
        }

        public void DrawLine(Point point, float length, float angle, GameColor color, float thickness)
        {
            Primitives2D.DrawLine(_spriteBatch, point.ToVector2(), length, angle, color.ToXnaColor(), thickness);
        }

        public void DrawLine(Point point1, Point point2, GameColor color)
        {
            Primitives2D.DrawLine(_spriteBatch, point1.ToVector2(), point2.ToVector2(), color.ToXnaColor());
        }

        public void DrawLine(Point point1, Point point2, GameColor color, float thickness)
        {
            Primitives2D.DrawLine(_spriteBatch, point1.ToVector2(), point2.ToVector2(), color.ToXnaColor(), thickness);
        }

        public void DrawRectangle(Rectangle rect, GameColor color)
        {
            Primitives2D.DrawRectangle(_spriteBatch, rect.ToXnaRectangle().ToRectangle(), color.ToXnaColor());
        }

        public void DrawRectangle(Rectangle rect, GameColor color, float thickness)
        {
            Primitives2D.DrawRectangle(_spriteBatch, rect.ToXnaRectangle().ToRectangle(), color.ToXnaColor(), thickness);
        }

        public void DrawRectangle(Point location, Point size, GameColor color)
        {
            Primitives2D.DrawRectangle(_spriteBatch, location.ToVector2(), size.ToVector2(), color.ToXnaColor());
        }

        public void DrawRectangle(Point location, Point size, GameColor color, float thickness)
        {
            Primitives2D.DrawRectangle(_spriteBatch, location.ToVector2(), size.ToVector2(), color.ToXnaColor(), thickness);
        }

        public void FillRectangle(float x, float y, float w, float h, GameColor color)
        {
            Primitives2D.FillRectangle(_spriteBatch, x, y, w, h, color.ToXnaColor());
        }

        public void FillRectangle(float x, float y, float w, float h, GameColor color, float angle)
        {
            Primitives2D.FillRectangle(_spriteBatch, x, y, w, h, color.ToXnaColor(), angle);
        }

        public void FillRectangle(Rectangle rect, GameColor color)
        {
            Primitives2D.FillRectangle(_spriteBatch, rect.ToXnaRectangle().ToRectangle(), color.ToXnaColor());
        }

        public void FillRectangle(Rectangle rect, GameColor color, float angle)
        {
            Primitives2D.FillRectangle(_spriteBatch, rect.ToXnaRectangle().ToRectangle(), color.ToXnaColor(), angle);
        }

        public void FillRectangle(Point location, Point size, GameColor color)
        {
            Primitives2D.FillRectangle(_spriteBatch, location.ToVector2(), size.ToVector2(), color.ToXnaColor());
        }

        public void FillRectangle(Point location, Point size, GameColor color, float angle)
        {
            Primitives2D.FillRectangle(_spriteBatch, location.ToVector2(), size.ToVector2(), color.ToXnaColor(), angle);    
        }

        public void PutPixel(float x, float y, GameColor color)
        {
            Primitives2D.PutPixel(_spriteBatch, x, y, color.ToXnaColor());
        }

        public void PutPixel(Point position, GameColor color)
        {
            Primitives2D.PutPixel(_spriteBatch, position.ToVector2(), color.ToXnaColor());
        }
    }
}
