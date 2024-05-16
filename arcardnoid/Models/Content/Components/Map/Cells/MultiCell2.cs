using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace arcardnoid.Models.Content.Components.Map.Cells
{
    internal class MultiCell2 : MapCell
    {
        #region Private Properties

        private int Size { get; set; }
        private RectangleF SourceRect { get; set; }
        private MultiCell2Type Type { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public MultiCell2(string name, Texture2D texture, int x, int y, int realX, int realY, int size, int offsetX, int offsetY, MultiCell2Type type) : base(name, texture, x, y, realX, realY, offsetX, offsetY)
        {
            Size = size;
            Type = type;
        }

        #endregion Public Constructors

        #region Public Methods

        public static MultiCell2Type GetMultiCellType(int[,] map, int x, int y, int width, int height)
        {
            // Single
            if (IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x + 1, y, width, height) && IsEmpty(map, x, y - 1, width, height) && !IsEmpty(map, x, y + 1, width, height) && IsEmpty(map, x, y - 2, width, height))
            {
                return MultiCell2Type.Single;
            }
            // Top Left
            else if (IsEmpty(map, x - 1, y, width, height) && !IsEmpty(map, x + 1, y, width, height) && IsEmpty(map, x, y - 1, width, height))
            {
                return MultiCell2Type.TopLeft;
            }
            // Top
            else if (!IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x, y - 1, width, height) && !IsEmpty(map, x + 1, y, width, height))
            {
                return MultiCell2Type.Top;
            }
            // Top Right
            else if (IsEmpty(map, x, y - 1, width, height) && !IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x + 1, y, width, height))
            {
                return MultiCell2Type.TopRight;
            }
            // Left
            else if (IsEmpty(map, x - 1, y, width, height) && !IsEmpty(map, x + 1, y, width, height) && !IsEmpty(map, x, y - 1, width, height) && !IsEmpty(map, x, y + 1, width, height) && !IsEmpty(map, x, y + 2, width, height) && !IsEmpty(map, x + 1, y, width, height))
            {
                return MultiCell2Type.Left;
            }
            // Center
            else if (!IsEmpty(map, x - 1, y, width, height) && !IsEmpty(map, x + 1, y, width, height) && !IsEmpty(map, x, y - 1, width, height) && !IsEmpty(map, x, y + 1, width, height) && !IsEmpty(map, x, y + 2, width, height))
            {
                return MultiCell2Type.Center;
            }
            // Right
            else if (IsEmpty(map, x + 1, y, width, height) && !IsEmpty(map, x, y - 1, width, height) && !IsEmpty(map, x, y + 1, width, height) && !IsEmpty(map, x - 1, y, width, height))
            {
                return MultiCell2Type.Right;
            }
            // Bottom Left
            else if (IsEmpty(map, x - 1, y, width, height) && !IsEmpty(map, x, y + 1, width, height) && IsEmpty(map, x, y + 2, width, height))
            {
                return MultiCell2Type.BottomLeft;
            }
            // Bottom
            else if (!IsEmpty(map, x - 1, y, width, height) && !IsEmpty(map, x + 1, y, width, height) && !IsEmpty(map, x, y + 1, width, height) && IsEmpty(map, x, y + 2, width, height))
            {
                return MultiCell2Type.Bottom;
            }
            // Bottom Right
            else if (IsEmpty(map, x + 1, y, width, height) && !IsEmpty(map, x, y + 1, width, height) && IsEmpty(map, x, y + 2, width, height))
            {
                return MultiCell2Type.BottomRight;
            }

            // Elevation Single
            else if (IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x + 1, y, width, height) && IsEmpty(map, x, y + 1, width, height))
            {
                return MultiCell2Type.ElevationSingle;
            }
            // Elevation Left
            else if (IsEmpty(map, x - 1, y, width, height) && !IsEmpty(map, x + 1, y, width, height) && IsEmpty(map, x, y + 1, width, height))
            {
                return MultiCell2Type.ElevationLeft;
            }
            // Elevation Right
            else if (IsEmpty(map, x + 1, y, width, height) && !IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x, y + 1, width, height))
            {
                return MultiCell2Type.ElevationRight;
            }
            // Elevation Center
            else if (!IsEmpty(map, x - 1, y, width, height) && IsEmpty(map, x, y + 1, width, height) && !IsEmpty(map, x + 1, y, width, height))
            {
                return MultiCell2Type.ElevationCenter;
            }

            return MultiCell2Type.None;
        }

        public override void Draw()
        {
            if (SourceRect.IsEmpty != false)
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
                case MultiCell2Type.TopLeft:
                    return new RectangleF(0, 0, Size, Size);

                case MultiCell2Type.Top:
                    return new RectangleF(Size, 0, Size, Size);

                case MultiCell2Type.TopRight:
                    return new RectangleF(Size * 2, 0, Size, Size);

                case MultiCell2Type.Left:
                    return new RectangleF(0, Size, Size, Size);

                case MultiCell2Type.Center:
                    return new RectangleF(Size, Size, Size, Size);

                case MultiCell2Type.Right:
                    return new RectangleF(Size * 2, Size, Size, Size);

                case MultiCell2Type.BottomLeft:
                    return new RectangleF(0, Size * 2, Size, Size);

                case MultiCell2Type.Bottom:
                    return new RectangleF(Size, Size * 2, Size, Size);

                case MultiCell2Type.BottomRight:
                    return new RectangleF(Size * 2, Size * 2, Size, Size);

                case MultiCell2Type.TopLeftBottom:
                    return new RectangleF(0, Size * 4, Size, Size);

                case MultiCell2Type.TopBottom:
                    return new RectangleF(Size, Size * 4, Size, Size);

                case MultiCell2Type.TopRightBottom:
                    return new RectangleF(Size * 2, Size * 4, Size, Size);

                case MultiCell2Type.LeftRightTop:
                    return new RectangleF(Size * 3, 0, Size, Size);

                case MultiCell2Type.LeftRight:
                    return new RectangleF(Size * 3, Size, Size, Size);

                case MultiCell2Type.LeftRightBottom:
                    return new RectangleF(Size * 3, Size * 2, Size, Size);

                case MultiCell2Type.Single:
                    return new RectangleF(Size * 3, Size * 4, Size, Size);

                case MultiCell2Type.ElevationLeft:
                    return new RectangleF(0, Size * 3, Size, Size);

                case MultiCell2Type.ElevationCenter:
                    return new RectangleF(Size, Size * 3, Size, Size);

                case MultiCell2Type.ElevationRight:
                    return new RectangleF(Size * 2, Size * 3, Size, Size);

                case MultiCell2Type.ElevationSingle:
                    return new RectangleF(Size * 3, Size * 3, Size, Size);

                case MultiCell2Type.StairsLeft:
                    return new RectangleF(0, Size * 7, Size, Size);

                case MultiCell2Type.StairsCenter:
                    return new RectangleF(Size, Size * 7, Size, Size);

                case MultiCell2Type.StairsRight:
                    return new RectangleF(Size * 2, Size * 7, Size, Size);

                case MultiCell2Type.StarsSingle:
                    return new RectangleF(Size * 3, Size * 7, Size, Size);
            }
            return RectangleF.Empty;
        }

        #endregion Private Methods
    }
}