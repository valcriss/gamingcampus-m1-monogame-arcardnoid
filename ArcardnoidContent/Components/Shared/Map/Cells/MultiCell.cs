using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.Shared.Map.Cells
{
    public class MultiCell : MapCell
    {
        #region Private Properties

        private int Size { get; set; }
        private Rectangle SourceRect { get; set; } = Rectangle.Empty;
        private MultiCellType Type { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public MultiCell(ITexture texture, int x, int y, int realX, int realY, int size, int offsetX, int offsetY, MultiCellType type) : base(texture, x, y, realX, realY, offsetX, offsetY)
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
            if (SourceRect != Rectangle.Empty)
                Texture2D.DrawTexture(DrawBounds, SourceRect, Color, Rotation, Origin);
        }

        public override void Load()
        {
            base.Load();
            SourceRect = CalculateRect();
            Bounds = new Rectangle(Bounds.X, Bounds.Y, Size, Size);
            Origin = new Point(Bounds.Width / 2, Bounds.Height / 2);
            DrawOrigin = new Point(0, 0);
            UpdateRenderBounds();
        }

        #endregion Public Methods

        #region Private Methods

        private static bool IsEmpty(int[,] map, int x, int y, int width, int height)
        {
            return x < 0 || x >= width || y < 0 || y >= height || map[x, y] == -1;
        }

        private Rectangle CalculateRect()
        {
            return Type switch
            {
                MultiCellType.TopLeft => new Rectangle(0, 0, Size, Size),
                MultiCellType.Top => new Rectangle(Size, 0, Size, Size),
                MultiCellType.TopRight => new Rectangle(Size * 2, 0, Size, Size),
                MultiCellType.Left => new Rectangle(0, Size, Size, Size),
                MultiCellType.Center => new Rectangle(Size, Size, Size, Size),
                MultiCellType.Right => new Rectangle(Size * 2, Size, Size, Size),
                MultiCellType.BottomLeft => new Rectangle(0, Size * 2, Size, Size),
                MultiCellType.Bottom => new Rectangle(Size, Size * 2, Size, Size),
                MultiCellType.BottomRight => new Rectangle(Size * 2, Size * 2, Size, Size),
                MultiCellType.TopLeftBottom => new Rectangle(0, Size * 3, Size, Size),
                MultiCellType.TopBottom => new Rectangle(Size, Size * 3, Size, Size),
                MultiCellType.TopRightBottom => new Rectangle(Size * 2, Size * 3, Size, Size),
                MultiCellType.LeftRightTop => new Rectangle(Size * 3, 0, Size, Size),
                MultiCellType.LeftRight => new Rectangle(Size * 3, Size, Size, Size),
                MultiCellType.LeftRightBottom => new Rectangle(Size * 3, Size * 2, Size, Size),
                MultiCellType.Single => new Rectangle(Size * 3, Size * 3, Size, Size),
                _ => Rectangle.Empty,
            };
        }

        #endregion Private Methods
    }
}