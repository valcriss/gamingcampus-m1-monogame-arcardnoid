using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidShared.Framework.Components.UI
{
    public class Cursor : GameComponent
    {
        #region Private Properties

        private TextureType CursorAsset { get; set; }
        private Image? CursorImage { get; set; }

        private Point Offset { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public Cursor(TextureType cursorAsset, Point offset) : base()
        {
            CursorAsset = cursorAsset;
            Offset = offset;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            CursorImage = AddGameComponent(new Image(CursorAsset, (int)Offset.X, (int)Offset.Y));
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            if (CursorImage == null) return;
            IMouseService mouseService = GameServiceProvider.GetService<IMouseService>();
            Point mousePosition = mouseService.GetMousePosition();
            Point mousePoint = ScreenManager.UIScale(mousePosition);
            CursorImage.Position = new Point(mousePoint.X + (int)Offset.X, mousePoint.Y + (int)Offset.Y);
        }

        #endregion Public Methods
    }
}