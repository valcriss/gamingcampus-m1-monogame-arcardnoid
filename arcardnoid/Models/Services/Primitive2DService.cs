using arcardnoid.Models.Framework.Tools;
using arcardnoid.Models.Tools;
using ArcardnoidShared.Framework.Drawing;
using MonoGame.Extended;

namespace arcardnoid.Models.Services
{
    public class Primitive2DService : IPrimitives2D
    {
        #region Private Properties

        private ArCardNoidGame Game { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public Primitive2DService(ArCardNoidGame game)
        {
            Game = game;
        }

        #endregion Public Constructors

        #region Public Methods

        public void DrawArc(Point center, float radius, int sides, float startingAngle, float radians, GameColor color)
        {
            Game.SpriteBatch.DrawArc(center.ToVector2(), radius, sides, startingAngle, radians, color.ToXnaColor());
        }

        public void DrawArc(Point center, float radius, int sides, float startingAngle, float radians, GameColor color, float thickness)
        {
            Game.SpriteBatch.DrawArc(center.ToVector2(), radius, sides, startingAngle, radians, color.ToXnaColor(), thickness);
        }

        public void DrawCircle(float x, float y, float radius, int sides, GameColor color)
        {
            Game.SpriteBatch.DrawCircle(new Microsoft.Xna.Framework.Vector2(x, y), radius, sides, color.ToXnaColor());
        }

        public void DrawCircle(float x, float y, float radius, int sides, GameColor color, float thickness)
        {
            Game.SpriteBatch.DrawCircle(new Microsoft.Xna.Framework.Vector2(x, y), radius, sides, color.ToXnaColor(), thickness);
        }

        public void DrawCircle(Point center, float radius, int sides, GameColor color)
        {
            Game.SpriteBatch.DrawCircle(center.ToVector2(), radius, sides, color.ToXnaColor());
        }

        public void DrawCircle(Point center, float radius, int sides, GameColor color, float thickness)
        {
            Game.SpriteBatch.DrawCircle(center.ToVector2(), radius, sides, color.ToXnaColor(), thickness);
        }

        public void DrawLine(float x1, float y1, float x2, float y2, GameColor color)
        {
            Game.SpriteBatch.DrawLine(new Microsoft.Xna.Framework.Vector2(x1, y1), new Microsoft.Xna.Framework.Vector2(x2, y2), color.ToXnaColor());
        }

        public void DrawLine(float x1, float y1, float x2, float y2, GameColor color, float thickness)
        {
            Game.SpriteBatch.DrawLine(new Microsoft.Xna.Framework.Vector2(x1, y1), new Microsoft.Xna.Framework.Vector2(x2, y2), color.ToXnaColor(), thickness);
        }

        public void DrawLine(Point point, float length, float angle, GameColor color)
        {
            Game.SpriteBatch.DrawLine(point.ToVector2(), length, angle, color.ToXnaColor());
        }

        public void DrawLine(Point point, float length, float angle, GameColor color, float thickness)
        {
            Game.SpriteBatch.DrawLine(point.ToVector2(), length, angle, color.ToXnaColor(), thickness);
        }

        public void DrawLine(Point point1, Point point2, GameColor color)
        {
            Game.SpriteBatch.DrawLine(point1.ToVector2(), point2.ToVector2(), color.ToXnaColor());
        }

        public void DrawLine(Point point1, Point point2, GameColor color, float thickness)
        {
            Game.SpriteBatch.DrawLine(point1.ToVector2(), point2.ToVector2(), color.ToXnaColor(), thickness);
        }

        public void DrawRectangle(Rectangle rect, GameColor color)
        {
            Game.SpriteBatch.DrawRectangle(rect.ToXnaRectangle().ToRectangle(), color.ToXnaColor());
        }

        public void DrawRectangle(Rectangle rect, GameColor color, float thickness)
        {
            Game.SpriteBatch.DrawRectangle(rect.ToXnaRectangle().ToRectangle(), color.ToXnaColor(), thickness);
        }

        public void DrawRectangle(Point location, Point size, GameColor color)
        {
            Game.SpriteBatch.DrawRectangle(location.ToVector2(), size.ToVector2(), color.ToXnaColor());
        }

        public void DrawRectangle(Point location, Point size, GameColor color, float thickness)
        {
            Game.SpriteBatch.DrawRectangle(location.ToVector2(), size.ToVector2(), color.ToXnaColor(), thickness);
        }

        public void FillRectangle(float x, float y, float w, float h, GameColor color)
        {
            Game.SpriteBatch.FillRectangle(x, y, w, h, color.ToXnaColor());
        }

        public void FillRectangle(float x, float y, float w, float h, GameColor color, float angle)
        {
            Primitives2D.FillRectangle(Game.SpriteBatch, x, y, w, h, color.ToXnaColor(), angle);
        }

        public void FillRectangle(Rectangle rect, GameColor color)
        {
            Game.SpriteBatch.FillRectangle(rect.ToXnaRectangle().ToRectangle(), color.ToXnaColor());
        }

        public void FillRectangle(Rectangle rect, GameColor color, float angle)
        {
            Game.SpriteBatch.FillRectangle(rect.ToXnaRectangle().ToRectangle(), color.ToXnaColor(), angle);
        }

        public void FillRectangle(Point location, Point size, GameColor color)
        {
            Game.SpriteBatch.FillRectangle(location.ToVector2(), size.ToVector2(), color.ToXnaColor());
        }

        public void FillRectangle(Point location, Point size, GameColor color, float angle)
        {
            Game.SpriteBatch.FillRectangle(location.ToVector2(), size.ToVector2(), color.ToXnaColor(), angle);
        }

        public void PutPixel(float x, float y, GameColor color)
        {
            Game.SpriteBatch.PutPixel(x, y, color.ToXnaColor());
        }

        public void PutPixel(Point position, GameColor color)
        {
            Game.SpriteBatch.PutPixel(position.ToVector2(), color.ToXnaColor());
        }

        #endregion Public Methods
    }
}