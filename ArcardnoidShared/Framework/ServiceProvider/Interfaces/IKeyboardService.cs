namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface IKeyboardService
    {
        #region Public Methods

        bool HasBeenPressed(string key);

        bool IsKeyDown(string key);

        void Update();

        #endregion Public Methods
    }
}