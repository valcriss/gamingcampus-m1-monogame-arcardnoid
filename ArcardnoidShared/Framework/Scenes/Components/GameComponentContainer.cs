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

        public virtual T AddGameComponent<T>(T gameComponent) where T : GameComponent
        {
            if (this is GameComponent)
            {
                gameComponent.Parent = (GameComponent)this;
            }
            GameComponents.Add(gameComponent);
            return gameComponent;
        }

        public void InnerDraw()
        {
            if (State == ElementState.Loaded && Visible)
            {
                Draw();
                foreach (var gameComponent in GameComponents)
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
            foreach (var gameComponent in GameComponents)
            {
                gameComponent.InnerLoad();
            }
            State = ElementState.Loaded;
        }

        public void InnerUnload()
        {
            State = ElementState.Unloading;
            Unload();
            foreach (var gameComponent in GameComponents)
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
                foreach (var gameComponent in GameComponents)
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

        public void RemoveAllGameComponents()
        {
            GameComponent[] gameComponents = GameComponents.ToArray();
            foreach (GameComponent gameComponent in gameComponents)
            {
                gameComponent?.InnerUnload();
            }
        }

        public void RemoveGameComponent(GameComponent gameComponent)
        {
            gameComponent.InnerUnload();
        }

        #endregion Public Methods
    }
}