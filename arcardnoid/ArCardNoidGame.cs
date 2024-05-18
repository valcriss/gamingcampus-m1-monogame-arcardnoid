using arcardnoid.Models.Services;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace arcardnoid
{
    public class ArCardNoidGame : Game
    {
        #region Public Properties

        public GraphicsDeviceManager Graphics { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        #endregion Public Properties

        #region Private Properties

        private IGameService GameService => GameServiceProvider.GetService<IGameService>();

        #endregion Private Properties

        #region Public Constructors

        public ArCardNoidGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            GameServiceProvider.RegisterService(new ScreenManagerService(this));
            GameServiceProvider.RegisterService(new RandomService());
            GameServiceProvider.RegisterService(new MouseService());
            GameServiceProvider.RegisterService(new KeyboardService());
            GameService.OnGameExit += () => Exit();
            GameService.InitializeGame();
            ScreenManager.Initialize(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
            Content.RootDirectory = "Content";
        }

        #endregion Public Constructors

        #region Protected Methods

        protected override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            GameService.DrawGame();
            SpriteBatch.End();
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            GameServiceProvider.RegisterService(new BitmapFontService(this));
            GameServiceProvider.RegisterService(new TextureService(this));
            GameServiceProvider.RegisterService(new Primitive2DService(SpriteBatch));
            GameService.LoadGameContent();
        }

        protected override void Update(GameTime gameTime)
        {
            GameServiceProvider.GetService<IMouseService>().Update();
            GameServiceProvider.GetService<IKeyboardService>().Update();
            GameService.UpdateGame((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        #endregion Protected Methods
    }
}