using ArcardnoidShared.Framework.Drawing;

namespace arcardnoid.Models.Framework.Tools
{
    public interface IPrimitives2D
    {
        #region Public Methods

        void DrawArc(Point center, float radius, int sides, float startingAngle, float radians, GameColor color);

        void DrawArc(Point center, float radius, int sides, float startingAngle, float radians, GameColor color, float thickness);

        void DrawCircle(float x, float y, float radius, int sides, GameColor color);

        void DrawCircle(float x, float y, float radius, int sides, GameColor color, float thickness);

        void DrawCircle(Point center, float radius, int sides, GameColor color);

        void DrawCircle(Point center, float radius, int sides, GameColor color, float thickness);

        void DrawLine(float x1, float y1, float x2, float y2, GameColor color);

        void DrawLine(float x1, float y1, float x2, float y2, GameColor color, float thickness);

        void DrawLine(Point point, float length, float angle, GameColor color);

        void DrawLine(Point point, float length, float angle, GameColor color, float thickness);

        void DrawLine(Point point1, Point point2, GameColor color);

        void DrawLine(Point point1, Point point2, GameColor color, float thickness);

        void DrawRectangle(Rectangle rect, GameColor color);

        void DrawRectangle(Rectangle rect, GameColor color, float thickness);

        void DrawRectangle(Point location, Point size, GameColor color);

        void DrawRectangle(Point location, Point size, GameColor color, float thickness);

        void FillRectangle(float x, float y, float w, float h, GameColor color);

        void FillRectangle(float x, float y, float w, float h, GameColor color, float angle);

        void FillRectangle(Rectangle rect, GameColor color);

        void FillRectangle(Rectangle rect, GameColor color, float angle);

        void FillRectangle(Point location, Point size, GameColor color);

        void FillRectangle(Point location, Point size, GameColor color, float angle);

        void PutPixel(float x, float y, GameColor color);

        void PutPixel(Point position, GameColor color);

        #endregion Public Methods
    }
}