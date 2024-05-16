using ArcardnoidShared.Framework.Scenes;

namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface IScenesManager
    {
        #region Public Methods

        void AddScene(Scene scene);

        void Draw();

        void RemoveScene(Scene scene);

        void SwitchScene(Scene oldScene, Scene newScene);

        void Update(float delta);

        #endregion Public Methods
    }
}