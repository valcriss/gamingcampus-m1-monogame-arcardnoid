using ArcardnoidShared.Framework.Scenes.Element;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace ArcardnoidShared.Framework.Scenes
{
    public class ScenesManager : IScenesManager
    {
        #region Private Fields

        private List<Scene> _scenes;

        #endregion Private Fields

        #region Public Constructors

        public ScenesManager()
        {
            _scenes = new List<Scene>();
        }

        #endregion Public Constructors

        #region Public Methods

        public void AddScene(Scene scene)
        {
            _scenes.Add(scene);
            scene.InnerLoad();
        }

        public virtual void Draw()
        {
            Scene[] scenes = _scenes.ToArray();
            foreach (var scene in scenes)
            {
                scene.InnerDraw();
            }
        }

        public void RemoveScene(Scene scene)
        {
            scene.InnerUnload();
        }

        public void SwitchScene(Scene oldScene, Scene newScene)
        {
            AddScene(newScene);
            RemoveScene(oldScene);
        }

        public virtual void Update(float delta)
        {
            Scene[] scenes = _scenes.ToArray();
            foreach (var scene in scenes)
            {
                scene.InnerUpdate(delta);
            }
            _scenes.RemoveAll(x => x.State == ElementState.Unloaded);
        }

        #endregion Public Methods
    }
}