using ArcardnoidContent.Components.GamePlay;
using ArcardnoidContent.Scenes;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidContent
{
    public class ArcardnoidGame : IGameService
    {
        #region Public Events

        public event Action? OnGameExit;

        #endregion Public Events

        #region Private Fields

        private readonly IScenesManager _scenesManager;

        #endregion Private Fields

        #region Public Constructors

        public ArcardnoidGame()
        {
            _scenesManager = GameServiceProvider.GetService<IScenesManager>();
        }

        #endregion Public Constructors

        #region Public Methods

        public void DrawGame()
        {
            _scenesManager.Draw();
        }

        public void ExitGame()
        {
            OnGameExit?.Invoke();
        }

        public void InitializeGame()
        {
            GameServiceProvider.GetService<IScreenManager>().SetSize(new Point(1920, 1080));
            GameServiceProvider.RegisterService(new GamePlay());
        }

        public void LoadGameContent()
        {
            _scenesManager.AddScene(new GameScene());
        }

        public void UpdateGame(float delta)
        {
            _scenesManager.Update(delta);
        }

        #endregion Public Methods
    }
}