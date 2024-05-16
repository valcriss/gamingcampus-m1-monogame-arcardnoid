using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Components.UI;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class PauseDialog : GameComponent
    {
        #region Private Properties

        private Action OnDebug { get; set; }
        private Action OnQuit { get; set; }
        private Action OnResume { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public PauseDialog(Action onResume, Action onDebug, Action onQuit) : base(672, 1081)
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
            AddGameComponent(new Frame("ui/banner", 0, 0, 576, 448));
            AddGameComponent(new BitmapText("fonts/band", "Jeu en pause", 288, 90, TextHorizontalAlign.Center, TextVerticalAlign.Center, GameColor.Black));
            int buttonStartY = 150;
            AddGameComponent(new Button("Reprendre", "ui/buttons/button-green-normal", "ui/buttons/button-green-hover", "ui/buttons/button-green-pressed", OnResume, 80, buttonStartY, 410, 64));
            AddGameComponent(new Button("Debug", "ui/buttons/button-blue-normal", "ui/buttons/button-blue-hover", "ui/buttons/button-blue-pressed", OnDebug, 80, buttonStartY + (70), 410, 64));
            AddGameComponent(new Button("Quitter", "ui/buttons/button-red-normal", "ui/buttons/button-red-hover", "ui/buttons/button-red-pressed", OnQuit, 80, buttonStartY + (70 * 2), 410, 64));
        }

        #endregion Public Methods
    }
}