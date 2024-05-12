using arcardnoid.Models.Framework.Scenes;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models.Framework.Components.Images
{
    public class ImagePart : Image
    {
        private Rectangle SourceRect { get; set; }
        public ImagePart(string name, string spriteAsset, int x, int y, Rectangle sourceRect) : base(name, spriteAsset, x, y)
        {
            SourceRect = sourceRect;
        }

        public override void Load()
        {
            base.Load();
            Bounds = new RectangleF(Bounds.X, Bounds.Y, SourceRect.Width, SourceRect.Height);
            ImageBounds = SourceRect;
            Origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);
            UpdateRenderBounds();
        }

        public void SetSourceRect(Rectangle sourceRect)
        {
            SourceRect = sourceRect;
            ImageBounds = SourceRect;
            Origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);
            Bounds = new RectangleF(Bounds.X, Bounds.Y, SourceRect.Width, SourceRect.Height);
            UpdateRenderBounds();
        }

        protected override void UpdateRenderBounds()
        {
            DrawBounds = new RectangleF(RealBounds.X - DrawOrigin.X, RealBounds.Y - DrawOrigin.Y, ImageBounds.Width, ImageBounds.Height);
        }
    }
}
