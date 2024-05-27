using ArcardnoidShared.Framework.Scenes.Element;

namespace ArcardnoidShared.Framework.Scenes.Components
{
    public class GameComponentContainer : Element.Element
    {
        #region Protected Properties

        protected List<GameComponent> GameComponents { get; set; }

        #endregion Protected Properties

        #region Public Constructors

        public GameComponentContainer()
        {
            GameComponents = new List<GameComponent>();
        }

        #endregion Public Constructors

        #region Public Methods

        public static void RemoveGameComponent(GameComponent gameComponent)
        {
            gameComponent.InnerUnload();
        }

        public virtual T AddGameComponent<T>(T gameComponent) where T : GameComponent
        {
            if (this is GameComponent component)
            {
                gameComponent.Parent = component;
            }
            GameComponents.Add(gameComponent);
            return gameComponent;
        }

        public void InnerDraw()
        {
            if (State == ElementState.Loaded && Visible)
            {
                Draw();
                GameComponent[] gameComponents = GameComponents.ToArray();
                foreach (var gameComponent in gameComponents)
                {
                    if (gameComponent.State == ElementState.Loaded)
                        gameComponent.InnerDraw();
                }
            }
        }

        public void InnerLoad()
        {
            State = ElementState.Loading;
            Load();
            GameComponent[] gameComponents = GameComponents.ToArray();
            foreach (var gameComponent in gameComponents)
            {
                gameComponent.InnerLoad();
            }
            State = ElementState.Loaded;
        }

        public void InnerUnload()
        {
            State = ElementState.Unloading;
            Unload();
            GameComponent[] gameComponents = GameComponents.ToArray();
            foreach (var gameComponent in gameComponents)
            {
                gameComponent.InnerUnload();
            }
            State = ElementState.Unloaded;
        }

        public void InnerUpdate(float delta)
        {
            if (State == ElementState.Loaded && Enabled)
            {
                Update(delta);
                GameComponent[] gameComponents = GameComponents.ToArray();
                foreach (var gameComponent in gameComponents)
                {
                    if (gameComponent.State == ElementState.Loaded && gameComponent.Enabled)
                    {
                        gameComponent.InnerUpdate(delta);
                    }
                    else if (gameComponent.State == ElementState.None)
                    {
                        gameComponent.InnerLoad();
                    }
                }
            }
        }

        public virtual T? MoveToFront<T>(T? gameComponent) where T : GameComponent?
        {
            if (gameComponent == null) return null;
            GameComponents.Remove(gameComponent);
            GameComponents.Add(gameComponent);
            return gameComponent;
        }

        public void RemoveAllGameComponents()
        {
            GameComponent[] gameComponents = GameComponents.ToArray();
            foreach (GameComponent gameComponent in gameComponents)
            {
                gameComponent?.InnerUnload();
            }
        }

        #endregion Public Methods
    }
}