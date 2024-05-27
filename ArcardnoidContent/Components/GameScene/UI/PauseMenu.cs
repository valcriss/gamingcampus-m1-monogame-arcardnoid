using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class PauseMenu : GameComponent
    {
        #region Private Properties

        private Action OnDebug { get; set; }
        private Action OnQuit { get; set; }
        private Action OnResume { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public PauseMenu(Action onResume, Action onDebug, Action onQuit) : base(672, 1081)
        {
            OnResume = onResume;
            OnDebug = onDebug;
            OnQuit = onQuit;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddGameComponent(new Frame(TextureType.UI_BANNER, 0, 0, 576, 448));
            AddGameComponent(new BitmapText(BitmapFontType.Default, "Jeu en pause", 288, 90, TextHorizontalAlign.Center, TextVerticalAlign.Center, GameColor.Black));
            int buttonStartY = 150;
            AddGameComponent(new Button("Reprendre", TextureType.UI_BUTTONS_BUTTON_GREEN_NORMAL, TextureType.UI_BUTTONS_BUTTON_GREEN_HOVER, TextureType.UI_BUTTONS_BUTTON_GREEN_PRESSED, OnResume, 80, buttonStartY, 410, 64));
            AddGameComponent(new Button("Debug", TextureType.UI_BUTTONS_BUTTON_BLUE_NORMAL, TextureType.UI_BUTTONS_BUTTON_BLUE_HOVER, TextureType.UI_BUTTONS_BUTTON_BLUE_PRESSED, OnDebug, 80, buttonStartY + (70), 410, 64));
            AddGameComponent(new Button("Quitter", TextureType.UI_BUTTONS_BUTTON_RED_NORMAL, TextureType.UI_BUTTONS_BUTTON_RED_HOVER, TextureType.UI_BUTTONS_BUTTON_RED_PRESSED, OnQuit, 80, buttonStartY + (70 * 2), 410, 64));
        }

        #endregion Public Methods
    }
}