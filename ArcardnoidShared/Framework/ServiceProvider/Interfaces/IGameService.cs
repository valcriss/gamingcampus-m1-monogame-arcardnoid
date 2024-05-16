namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface IGameService
    {
        #region Public Events

        event Action OnGameExit;

        #endregion Public Events

        #region Public Methods

        void DrawGame();

        void ExitGame();

        void InitializeGame();

        void LoadGameContent();

        void UpdateGame(float delta);

        #endregion Public Methods
    }
}