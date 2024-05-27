using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class GameSceneUI : GameComponent
    {
        #region Public Constructors

        public GameSceneUI() : base()
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddGameComponent(new Image(TextureType.UI_HEADER, 300, 55));
            AddGameComponent(new HeartUI(120, 17));
            AddGameComponent(new HeartUI(195, 17));
            AddGameComponent(new CoinAmountUI(300, 5));
        }

        #endregion Public Methods
    }
}