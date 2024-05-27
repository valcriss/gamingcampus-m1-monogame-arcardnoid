using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.MainMenu
{
    public class MainGameMenu : GameComponent
    {
        #region Private Properties

        private Action OnCredits { get; set; }
        private Action OnDemo { get; set; }
        private Action OnNewGame { get; set; }
        private Action OnParameters { get; set; }
        private Action OnQuit { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public MainGameMenu(Action onNewGame, Action onCredits, Action onParameters, Action onDemo, Action onQuit) : base(672, 316)
        {
            OnNewGame = onNewGame;
            OnCredits = onCredits;
            OnParameters = onParameters;
            OnDemo = onDemo;
            OnQuit = onQuit;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddGameComponent(new Frame(TextureType.UI_BANNER, 0, 0, 576, 576));
            AddGameComponent(new Button("Commencer une partie", TextureType.UI_BUTTONS_BUTTON_GREEN_NORMAL, TextureType.UI_BUTTONS_BUTTON_GREEN_HOVER, TextureType.UI_BUTTONS_BUTTON_GREEN_PRESSED, OnNewGame, 80, 75, 410, 64));
            AddGameComponent(new Button("Crédits", TextureType.UI_BUTTONS_BUTTON_YELLOW_NORMAL, TextureType.UI_BUTTONS_BUTTON_YELLOW_HOVER, TextureType.UI_BUTTONS_BUTTON_YELLOW_PRESSED, OnCredits, 80, 155, 410, 64));
            AddGameComponent(new Button("Parametres", TextureType.UI_BUTTONS_BUTTON_BLUE_NORMAL, TextureType.UI_BUTTONS_BUTTON_BLUE_HOVER, TextureType.UI_BUTTONS_BUTTON_BLUE_PRESSED, OnParameters, 80, 235, 410, 64));
            AddGameComponent(new Button("Démo", TextureType.UI_BUTTONS_BUTTON_GREEN_NORMAL, TextureType.UI_BUTTONS_BUTTON_GREEN_HOVER, TextureType.UI_BUTTONS_BUTTON_GREEN_PRESSED, OnDemo, 80, 315, 410, 64));
            AddGameComponent(new Button("Quitter", TextureType.UI_BUTTONS_BUTTON_RED_NORMAL, TextureType.UI_BUTTONS_BUTTON_RED_HOVER, TextureType.UI_BUTTONS_BUTTON_RED_PRESSED, OnQuit, 80, 425, 410, 64));
        }

        #endregion Public Methods
    }
}