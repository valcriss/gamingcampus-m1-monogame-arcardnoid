using arcardnoid.Models.Framework.Configuration;
using arcardnoid.Models.Framework.Scenes;
using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace arcardnoid.Models.Framework
{
    public class BaseGame : Game
    {
        #region Public Properties

        public static bool DebugMode { get; set; } = false;
        public GameConfiguration Configuration { get; set; }

        public GraphicsDeviceManager Graphics { get; set; }

        public ScenesManager ScenesManager { get; set; }

        public SpriteBatch SpriteBatch { get; set; }

        #endregion Public Properties

        #region Private Properties

        private Scene InitialScene { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public BaseGame()
        {
            ScenesManager = new ScenesManager(this);
            Configuration = new GameConfiguration();
            Configuration.Load();
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = Configuration.ResolutionWidth;
            Graphics.PreferredBackBufferHeight = Configuration.ResolutionHeight;
            ScreenManager.Initialize(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
            IsMouseVisible = false;
            //IsFixedTimeStep = false;
            Graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        #endregion Public Constructors

        #region Protected Methods

        protected override void Draw(GameTime gameTime)
        {
            ScenesManager.Draw();
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            ScenesManager.AddScene(InitialScene);
        }

        protected void SetInitialScene(Scene scene)
        {
            InitialScene = scene;
        }

        protected override void Update(GameTime gameTime)
        {
            ScenesManager.Update(gameTime);
            base.Update(gameTime);
        }

        #endregion Protected Methods
    }
}