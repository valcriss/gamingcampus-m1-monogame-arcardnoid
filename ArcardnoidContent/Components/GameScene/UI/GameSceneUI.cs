using ArcardnoidContent.Components.GamePlay;
using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class GameSceneUI : GameComponent
    {
        #region Private Properties

        private IGamePlay GamePlay => GameServiceProvider.GetService<IGamePlay>();
        private HeartUI? HeartUI1 { get; set; } = null;
        private HeartUI? HeartUI2 { get; set; } = null;

        #endregion Private Properties

        #region Public Constructors

        public GameSceneUI() : base(-75, 0)
        {
            GamePlay.HeartChanged += OnHeartChanged;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            AddGameComponent(new Image(TextureType.UI_HEADER, 550, 55));
            HeartUI1 = AddGameComponent(new HeartUI(180, 17));
            HeartUI2 = AddGameComponent(new HeartUI(255, 17));
            AddGameComponent(new CoinAmountUI(350, 10));
            AddGameComponent(new UnitsCountUI(540, 10));
            AddGameComponent(new CardsCountUI(770, 10));
        }

        #endregion Public Methods

        #region Private Methods

        private void OnHeartChanged(int heart, int change)
        {
            if (change > 0)
            {
                if (HeartUI2?.HeartState == HearState.Empty)
                {
                    HeartUI2.HeartState = HearState.Filling;
                }
            }
            else if (change < 1)
            {
                if (HeartUI2?.HeartState == HearState.Full)
                {
                    HeartUI2.HeartState = HearState.Emptying;
                }
                else if (HeartUI1?.HeartState == HearState.Full)
                {
                    HeartUI1.HeartState = HearState.Emptying;
                }
            }
        }

        #endregion Private Methods
    }
}