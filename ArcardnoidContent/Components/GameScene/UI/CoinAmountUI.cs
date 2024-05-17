using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class CoinAmountUI : GameComponent
    {
        #region Private Properties

        private int CoinAmount { get; set; } = 100;

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
            AddGameComponent(new Image("ui/coin", 0, 25));
            AddGameComponent(new BitmapText("fonts/band", CoinAmount.ToString() + " Or", 42, 17, TextHorizontalAlign.Left, TextVerticalAlign.Top, GameColor.Black));
            AddGameComponent(new BitmapText("fonts/band", CoinAmount.ToString() + " Or", 40, 15, TextHorizontalAlign.Left, TextVerticalAlign.Top, GameColor.White));
        }

        #endregion Public Methods
    }
}