using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace arcardnoid.Models.Framework.Scenes
{
    public class ScenesManager : ElementManager
    {
        #region Private Fields

        private List<Scene> _scenes;

        #endregion Private Fields

        #region Public Constructors

        public ScenesManager(BaseGame game) : base(game)
        {
            _scenes = new List<Scene>();
        }

        #endregion Public Constructors

        #region Public Methods

        public void AddScene(Scene scene)
        {
            scene.SetGame(Game);
            _scenes.Add(scene);
            scene.InnerLoad();
        }

        public virtual void Draw()
        {
            Game.SpriteBatch.Begin();
            Scene[] scenes = _scenes.ToArray();
            foreach (var scene in scenes)
            {
                scene.InnerDraw();
            }
            Game.SpriteBatch.End();
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

        public virtual void Update(GameTime gameTime)
        {
            Scene[] scenes = _scenes.ToArray();
            foreach (var scene in scenes)
            {
                scene.InnerUpdate(gameTime);
            }
            _scenes.RemoveAll(x => x.State == ElementState.Unloaded);
        }

        #endregion Public Methods
    }
}