using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Scenes.Components;

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
            AddGameComponent(new Frame("ui/banner", 0, 0, 576, 576));
            AddGameComponent(new Button("Commencer une partie", "ui/buttons/button-green-normal", "ui/buttons/button-green-hover", "ui/buttons/button-green-pressed", OnNewGame, 80, 75, 410, 64));
            AddGameComponent(new Button("Crédits", "ui/buttons/button-yellow-normal", "ui/buttons/button-yellow-hover", "ui/buttons/button-yellow-pressed", OnCredits, 80, 155, 410, 64));
            AddGameComponent(new Button("Parametres", "ui/buttons/button-blue-normal", "ui/buttons/button-blue-hover", "ui/buttons/button-blue-pressed", OnParameters, 80, 235, 410, 64));
            AddGameComponent(new Button("Démo", "ui/buttons/button-green-normal", "ui/buttons/button-green-hover", "ui/buttons/button-green-pressed", OnDemo, 80, 315, 410, 64));
            AddGameComponent(new Button("Quitter", "ui/buttons/button-red-normal", "ui/buttons/button-red-hover", "ui/buttons/button-red-pressed", OnQuit, 80, 425, 410, 64));
        }

        #endregion Public Methods
    }
}