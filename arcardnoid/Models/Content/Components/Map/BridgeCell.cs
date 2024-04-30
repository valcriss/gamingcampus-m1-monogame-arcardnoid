using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace arcardnoid.Models.Content.Components.Map
{
    internal class BridgeCell : MapCell
    {
        #region Private Properties

        private int Size { get; set; }
        private RectangleF SourceRect { get; set; }
        private BridgeCellType Type { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public BridgeCell(string name, Texture2D texture, int x, int y, int realX, int realY, int size, int offsetX, int offsetY, BridgeCellType type) : base(name, texture, x, y, realX, realY, offsetX, offsetY)
        {
            Size = size;
            Type = type;
        }

        #endregion Public Constructors

        #region Public Methods

        public static BridgeCellType GetMultiCellType(int[,] map, int x, int y, int width, int height)
        {
            if (IsEmpty(map, x - 1, y, width, height) && !IsBorder(map, x - 1, y, width, height) && !IsBorder(map, x, y + 1, width, height) && !IsBorder(map, x, y - 1, width, height) && IsEmpty(map, x, y - 1, width, height) && IsEmpty(map, x, y + 1, width, height))
            {
                return BridgeCellType.HorizontalLeft;
            }
            else if ((!IsEmpty(map, x - 1, y, width, height) || IsBorder(map, x - 1, y, width, height)) && IsEmpty(map, x, y - 1, width, height) && (!IsEmpty(map, x + 1, y, width, height) || IsBorder(map, x + 1, y, width, height)))
            {
                return BridgeCellType.HorizontalCenter;
            }
            else if ((!IsEmpty(map, x - 1, y, width, height) || IsBorder(map, x - 1, y, width, height)) && IsEmpty(map, x, y - 1, width, height) && IsEmpty(map, x, y + 1, width, height) && IsEmpty(map, x + 1, y, width, height) && !IsBorder(map, x + 1, y, width, height))
            {
                return BridgeCellType.HorizontalRight;
            }
            else if (IsEmpty(map, x, y - 1, width, height) && !IsBorder(map, x, y - 1, width, height) && IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x + 1, y, width, height))
            {
                return BridgeCellType.VerticalTop;
            }
            else if ((!IsEmpty(map, x, y - 1, width, height) || IsBorder(map, x, y - 1, width, height)) && IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x + 1, y, width, height) && (!IsEmpty(map, x, y + 1, width, height) || IsBorder(map, x, y + 1, width, height)))
            {
                return BridgeCellType.VerticalCenter;
            }
            else if ((!IsEmpty(map, x - 1, y - 1, width, height) || IsBorder(map, x - 1, y - 1, width, height) || !IsEmpty(map, x + 1, y - 1, width, height) || IsBorder(map, x + 1, y - 1, width, height)) && !IsEmpty(map, x, y - 1, width, height))
            {
                return BridgeCellType.BridgeShadow;
            }
            else if ((!IsEmpty(map, x, y - 1, width, height) || IsBorder(map, x, y - 1, width, height)) && IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x + 1, y, width, height) && IsEmpty(map, x, y + 1, width, height) && !IsBorder(map, x, y + 1, width, height))
            {
                return BridgeCellType.VerticalBottom;
            }

            return BridgeCellType.None;
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

        private static bool IsBorder(int[,] map, int x, int y, int width, int height)
        {
            return x < 0 || x >= width || y < 0 || y >= height;
        }

        private static bool IsEmpty(int[,] map, int x, int y, int width, int height)
        {
            return x < 0 || x >= width || y < 0 || y >= height || map[x, y] == -1;
        }

        private RectangleF CalculateRect()
        {
            switch (Type)
            {
                case BridgeCellType.HorizontalLeft:
                    return new RectangleF(0, 0, Size, Size);

                case BridgeCellType.HorizontalCenter:
                    return new RectangleF(Size, 0, Size, Size);

                case BridgeCellType.HorizontalRight:
                    return new RectangleF(Size * 2, 0, Size, Size);

                case BridgeCellType.VerticalTop:
                    return new RectangleF(0, Size, Size, Size);

                case BridgeCellType.VerticalCenter:
                    return new RectangleF(0, Size * 2, Size, Size);

                case BridgeCellType.VerticalBottom:
                    return new RectangleF(0, Size * 3, Size, Size);

                case BridgeCellType.BridgeShadow:
                    return new RectangleF(Size * 2, Size * 3, Size, Size);
            }
            return RectangleF.Empty;
        }

        #endregion Private Methods
    }
}