using arcardnoid.Models.Framework.Components.UI;
using arcardnoid.Models.Framework.Scenes;
using System;

namespace arcardnoid.Models.Content.Components.MainMenu
{
    public class MainGameMenu : Component
    {
        #region Private Properties

        private Action OnCredits { get; set; }
        private Action OnNewGame { get; set; }
        private Action OnParameters { get; set; }
        private Action OnQuit { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public MainGameMenu(Action onNewGame, Action onCredits, Action onParameters, Action onQuit) : base("MainGameMenu", 672, 316)
        {
            OnNewGame = onNewGame;
            OnCredits = onCredits;
            OnParameters = onParameters;
            OnQuit = onQuit;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddComponent(new Frame("frame", "ui/banner", 0, 0, 576, 448));
            int buttonStartY = 75;
            AddComponent(new Button("btnPlay", "Commencer une partie", "ui/buttons/button-green-normal", "ui/buttons/button-green-hover", "ui/buttons/button-green-pressed", OnNewGame, 80, buttonStartY, 410, 64));
            AddComponent(new Button("btnCredits", "Credits", "ui/buttons/button-yellow-normal", "ui/buttons/button-yellow-hover", "ui/buttons/button-yellow-pressed", OnCredits, 80, buttonStartY + 80, 410, 64));
            AddComponent(new Button("btnParametres", "Parametres", "ui/buttons/button-blue-normal", "ui/buttons/button-blue-hover", "ui/buttons/button-blue-pressed", OnParameters, 80, buttonStartY + (80 * 2), 410, 64));
            AddComponent(new Button("btnQuit", "Quitter", "ui/buttons/button-red-normal", "ui/buttons/button-red-hover", "ui/buttons/button-red-pressed", OnQuit, 80, buttonStartY + (80 * 3), 410, 64));
        }

        #endregion Public Methods
    }
}