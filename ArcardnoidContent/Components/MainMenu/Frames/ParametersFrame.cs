using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.MainMenu.Frames
{
    public class ParametersFrame : GameComponent
    {
        #region Private Properties

        private Progress? MusicVolume { get; set; }
        private Action OnClose { get; set; }
        private Progress? SoundVolume { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public ParametersFrame(Action onClose) : base(672, 1081)
        {
            OnClose = onClose;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            int height = 428;
            AddGameComponent(new Frame(TextureType.UI_BANNER, -195, 0, 960, height));
            AddGameComponent(new BitmapText(BitmapFontType.Default, "Parametres", 288, 90, TextHorizontalAlign.Center, TextVerticalAlign.Center, GameColor.Black));

            int y = 165;
            int x = -115;
            AddGameComponent(new BitmapText(BitmapFontType.Default, "Volume musique:", x, y, TextHorizontalAlign.Left, TextVerticalAlign.Center, GameColor.Black));
            MusicVolume = AddGameComponent(new Progress(TextureType.UI_PROGRESS_PROGRESS_NORMAL, TextureType.UI_PROGRESS_PROGRESS_HOVER, TextureType.UI_PROGRESS_PROGRESS_PRESSED, 0.5f, x + 295, y - 25, 6));

            y += 80;
            AddGameComponent(new BitmapText(BitmapFontType.Default, "Volume effets:", x, y, TextHorizontalAlign.Left, TextVerticalAlign.Center, GameColor.Black));
            SoundVolume = AddGameComponent(new Progress(TextureType.UI_PROGRESS_PROGRESS_NORMAL, TextureType.UI_PROGRESS_PROGRESS_HOVER, TextureType.UI_PROGRESS_PROGRESS_PRESSED, 0.5f, x + 295, y - 25, 6));

            AddGameComponent(new Button("Annuler", TextureType.UI_BUTTONS_BUTTON_RED_NORMAL, TextureType.UI_BUTTONS_BUTTON_RED_HOVER, TextureType.UI_BUTTONS_BUTTON_RED_PRESSED, OnClose, 490, height - 124, 210, 64));
        }

        #endregion Public Methods
    }
}