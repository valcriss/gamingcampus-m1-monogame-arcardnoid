using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace arcardnoid.Models.Content.Components.Map.Cells
{
    internal class MultiCell : MapCell
    {
        #region Private Properties

        private int Size { get; set; }
        private RectangleF SourceRect { get; set; }
        private MultiCellType Type { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public MultiCell(string name, Texture2D texture, int x, int y, int realX, int realY, int size, int offsetX, int offsetY, MultiCellType type) : base(name, texture, x, y, realX, realY, offsetX, offsetY)
        {
            Size = size;
            Type = type;
        }

        #endregion Public Constructors

        #region Public Methods

        public static MultiCellType GetMultiCellType(int[,] map, int x, int y, int width, int height)
        {
            // Single
            if (IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x + 1, y, width, height) && IsEmpty(map, x, y - 1, width, height) && IsEmpty(map, x, y + 1, width, height))
            {
                return MultiCellType.Single;
            }
            // Top Left
            else if (IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x, y - 1, width, height))
            {
                return MultiCellType.TopLeft;
            }
            // Top Right
            else if (IsEmpty(map, x, y - 1, width, height) && IsEmpty(map, x + 1, y, width, height) && !IsEmpty(map, x - 1, y, width, height))
            {
                return MultiCellType.TopRight;
            }
            // Top
            else if (IsEmpty(map, x, y - 1, width, height) && !IsEmpty(map, x - 1, y, width, height) && !IsEmpty(map, x + 1, y, width, height))
            {
                return MultiCellType.Top;
            }
            // Left
            else if (IsEmpty(map, x - 1, y, width, height) && !IsEmpty(map, x, y - 1, width, height) && !IsEmpty(map, x, y + 1, width, height) && !IsEmpty(map, x + 1, y, width, height))
            {
                return MultiCellType.Left;
            }
            // Center
            else if (!IsEmpty(map, x - 1, y, width, height) && !IsEmpty(map, x, y - 1, width, height) && !IsEmpty(map, x + 1, y, width, height) && !IsEmpty(map, x, y + 1, width, height))
            {
                return MultiCellType.Center;
            }
            // Right
            else if (IsEmpty(map, x + 1, y, width, height) && !IsEmpty(map, x, y - 1, width, height) && !IsEmpty(map, x, y + 1, width, height) && !IsEmpty(map, x - 1, y, width, height))
            {
                return MultiCellType.Right;
            }
            // Bottom Left
            else if (IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x, y + 1, width, height) && !IsEmpty(map, x, y - 1, width, height))
            {
                return MultiCellType.BottomLeft;
            }
            // Bottom Right
            else if (IsEmpty(map, x, y + 1, width, height) && IsEmpty(map, x + 1, y, width, height) && !IsEmpty(map, x - 1, y, width, height))
            {
                return MultiCellType.BottomRight;
            }
            // Bottom
            else if (IsEmpty(map, x, y + 1, width, height) && !IsEmpty(map, x - 1, y, width, height) && !IsEmpty(map, x + 1, y, width, height))
            {
                return MultiCellType.Bottom;
            }

            return MultiCellType.None;
        }

        public override void Draw()
        {
            if (SourceRect.IsEmpty == false)
                Game.SpriteBatch.Draw(Texture2D, ScreenManager.Scale(DrawBounds).ToRectangle(), SourceRect.ToRectangle(), Color, MathHelper.ToRadians(Rotation), Origin, SpriteEffects.None, 0);
        }

        public override void Load()
        {
            base.Load();
            SourceRect = CalculateRect();
            Bounds = new RectangleF(Bounds.X, Bounds.Y, Size, Size);
            Origin = new Vector2(Bounds.Width / 2, Bounds.Height / 2);
            DrawOrigin = new Vector2(0, 0);
            UpdateRenderBounds();
        }

        #endregion Public Methods

        #region Private Methods

        private static bool IsEmpty(int[,] map, int x, int y, int width, int height)
        {
            return x < 0 || x >= width || y < 0 || y >= height || map[x, y] == -1;
        }

        private RectangleF CalculateRect()
        {
            switch (Type)
            {
                case MultiCellType.TopLeft:
                    return new RectangleF(0, 0, Size, Size);

                case MultiCellType.Top:
                    return new RectangleF(Size, 0, Size, Size);

                case MultiCellType.TopRight:
                    return new RectangleF(Size * 2, 0, Size, Size);

                case MultiCellType.Left:
                    return new RectangleF(0, Size, Size, Size);

                case MultiCellType.Center:
                    return new RectangleF(Size, Size, Size, Size);

                case MultiCellType.Right:
                    return new RectangleF(Size * 2, Size, Size, Size);

                case MultiCellType.BottomLeft:
                    return new RectangleF(0, Size * 2, Size, Size);

                case MultiCellType.Bottom:
                    return new RectangleF(Size, Size * 2, Size, Size);

                case MultiCellType.BottomRight:
                    return new RectangleF(Size * 2, Size * 2, Size, Size);

                case MultiCellType.TopLeftBottom:
                    return new RectangleF(0, Size * 3, Size, Size);

                case MultiCellType.TopBottom:
                    return new RectangleF(Size, Size * 3, Size, Size);

                case MultiCellType.TopRightBottom:
                    return new RectangleF(Size * 2, Size * 3, Size, Size);

                case MultiCellType.LeftRightTop:
                    return new RectangleF(Size * 3, 0, Size, Size);

                case MultiCellType.LeftRight:
                    return new RectangleF(Size * 3, Size, Size, Size);

                case MultiCellType.LeftRightBottom:
                    return new RectangleF(Size * 3, Size * 2, Size, Size);

                case MultiCellType.Single:
                    return new RectangleF(Size * 3, Size * 3, Size, Size);
            }
            return RectangleF.Empty;
        }

        #endregion Private Methods
    }
}