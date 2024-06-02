using ArcardnoidContent.Components.GamePlay;
using ArcardnoidContent.Components.GamePlay.Cards;
using ArcardnoidShared.Framework.Components.Images;
using ArcardnoidShared.Framework.Components.Text;
using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Enums;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class CardsCountUI : GameComponent
    {
        #region Private Properties

        private BitmapText[] BitmapTexts { get; set; } = new BitmapText[2];
        private int CardsCount { get; set; } = 0;

        #endregion Private Properties

        #region Public Constructors

        public CardsCountUI(int x, int y) : base(x, y)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Load()
        {
            base.Load();
            IGamePlay gamePlay = GameServiceProvider.GetService<IGamePlay>();
            gamePlay.CardChanged += GamePlay_CardChanged;
            CardsCount = gamePlay.GetCards().Count;
            AddGameComponent(new Image(TextureType.UI_CARDS, 10, 35));
            BitmapTexts[0] = AddGameComponent(new BitmapText(BitmapFontType.Default, CardsCount.ToString() + " Sorts", 42, 17, TextHorizontalAlign.Left, TextVerticalAlign.Top, GameColor.Black));
            BitmapTexts[1] = AddGameComponent(new BitmapText(BitmapFontType.Default, CardsCount.ToString() + " Sorts", 40, 15, TextHorizontalAlign.Left, TextVerticalAlign.Top, GameColor.White));
        }

        #endregion Public Methods

        #region Private Methods

        private void GamePlay_CardChanged(List<Card> cards)
        {
            for (int i = 0; i < BitmapTexts.Length; i++)
            {
                BitmapTexts[i].SetText(cards.Count.ToString() + " Sorts");
            }
        }

        #endregion Private Methods
    }
}