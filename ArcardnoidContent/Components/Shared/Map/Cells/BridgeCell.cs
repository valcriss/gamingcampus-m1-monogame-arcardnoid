using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent.Components.Shared.Map.Cells
{
    public class BridgeCell : MapCell
    {
        #region Private Properties

        private int Size { get; set; }
        private Rectangle SourceRect { get; set; } = Rectangle.Empty;
        private BridgeCellType Type { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public BridgeCell(ITexture texture, int x, int y, int realX, int realY, int size, int offsetX, int offsetY, BridgeCellType type) : base(texture, x, y, realX, realY, offsetX, offsetY)
        {
            Size = size;
            Type = type;
        }

        #endregion Public Constructors

        #region Public Methods

        public static BridgeCellType GetMultiCellType(int[,] map, int x, int y, int width, int height)
        {
            if (IsEmpty(map, x - 1, y, width, height) && !IsBorder(x - 1, y, width, height) && !IsBorder(x, y + 1, width, height) && !IsBorder(x, y - 1, width, height) && IsEmpty(map, x, y - 1, width, height) && IsEmpty(map, x, y + 1, width, height))
            {
                return BridgeCellType.HorizontalLeft;
            }
            else if ((!IsEmpty(map, x - 1, y, width, height) || IsBorder(x - 1, y, width, height)) && IsEmpty(map, x, y - 1, width, height) && (!IsEmpty(map, x + 1, y, width, height) || IsBorder(x + 1, y, width, height)))
            {
                return BridgeCellType.HorizontalCenter;
            }
            else if ((!IsEmpty(map, x - 1, y, width, height) || IsBorder(x - 1, y, width, height)) && IsEmpty(map, x, y - 1, width, height) && IsEmpty(map, x, y + 1, width, height) && IsEmpty(map, x + 1, y, width, height) && !IsBorder(x + 1, y, width, height))
            {
                return BridgeCellType.HorizontalRight;
            }
            else if (IsEmpty(map, x, y - 1, width, height) && !IsBorder(x, y - 1, width, height) && IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x + 1, y, width, height))
            {
                return BridgeCellType.VerticalTop;
            }
            else if ((!IsEmpty(map, x, y - 1, width, height) || IsBorder(x, y - 1, width, height)) && IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x + 1, y, width, height) && (!IsEmpty(map, x, y + 1, width, height) || IsBorder(x, y + 1, width, height)))
            {
                return BridgeCellType.VerticalCenter;
            }
            else if ((!IsEmpty(map, x - 1, y - 1, width, height) || IsBorder(x - 1, y - 1, width, height) || !IsEmpty(map, x + 1, y - 1, width, height) || IsBorder(x + 1, y - 1, width, height)) && !IsEmpty(map, x, y - 1, width, height))
            {
                return BridgeCellType.BridgeShadow;
            }
            else if ((!IsEmpty(map, x, y - 1, width, height) || IsBorder(x, y - 1, width, height)) && IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x + 1, y, width, height) && IsEmpty(map, x, y + 1, width, height) && !IsBorder(x, y + 1, width, height))
            {
                return BridgeCellType.VerticalBottom;
            }

            return BridgeCellType.None;
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

        private static bool IsBorder(int x, int y, int width, int height)
        {
            return x < 0 || x >= width || y < 0 || y >= height;
        }

        private static bool IsEmpty(int[,] map, int x, int y, int width, int height)
        {
            return x < 0 || x >= width || y < 0 || y >= height || map[x, y] == -1;
        }

        private Rectangle CalculateRect()
        {
            return Type switch
            {
                BridgeCellType.HorizontalLeft => new Rectangle(0, 0, Size, Size),
                BridgeCellType.HorizontalCenter => new Rectangle(Size, 0, Size, Size),
                BridgeCellType.HorizontalRight => new Rectangle(Size * 2, 0, Size, Size),
                BridgeCellType.VerticalTop => new Rectangle(0, Size, Size, Size),
                BridgeCellType.VerticalCenter => new Rectangle(0, Size * 2, Size, Size),
                BridgeCellType.VerticalBottom => new Rectangle(0, Size * 3, Size, Size),
                BridgeCellType.BridgeShadow => new Rectangle(Size * 2, Size * 3, Size, Size),
                _ => Rectangle.Empty,
            };
        }

        #endregion Private Methods
    }
}