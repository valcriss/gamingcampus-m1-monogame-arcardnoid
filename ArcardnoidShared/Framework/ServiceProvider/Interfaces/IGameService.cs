namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface IGameService
    {
        #region Public Methods
        
        event Action OnGameExit;

        void DrawGame();

        void InitializeGame();

        void LoadGameContent();

        void UpdateGame(float delta);

        void ExitGame();

        #endregion Public Methods
    }
}