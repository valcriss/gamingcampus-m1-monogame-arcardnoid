using ArcardnoidContent.Components.GamePlay;
using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class CoinAmountUI : GameComponent
    {
        #region Private Properties

        private BitmapText[] BitmapTexts { get; set; } = new BitmapText[2];
        private int CoinAmount { get; set; } = 0;

        #endregion Private Properties

        #region Public Constructors

        public CoinAmountUI(int x, int y) : base(x, y)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            IGamePlay gamePlay = GameServiceProvider.GetService<IGamePlay>();
            gamePlay.GoldChanged += GamePlay_GoldChanged;
            CoinAmount = gamePlay.GetGold();
            AddGameComponent(new Image("ui/coin", 0, 25));
            BitmapTexts[0] = AddGameComponent(new BitmapText("fonts/band", CoinAmount.ToString() + " Or", 42, 17, TextHorizontalAlign.Left, TextVerticalAlign.Top, GameColor.Black));
            BitmapTexts[1] = AddGameComponent(new BitmapText("fonts/band", CoinAmount.ToString() + " Or", 40, 15, TextHorizontalAlign.Left, TextVerticalAlign.Top, GameColor.White));
        }

        #endregion Public Methods

        #region Private Methods

        private void GamePlay_GoldChanged(int gold)
        {
            for (int i = 0; i < BitmapTexts.Length; i++)
            {
                BitmapTexts[i].SetText(gold.ToString() + " Or");
            }
        }

        #endregion Private Methods
    }
}