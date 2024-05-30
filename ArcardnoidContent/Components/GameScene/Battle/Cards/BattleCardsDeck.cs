using ArcardnoidContent.Components.GamePlay;
using ArcardnoidContent.Components.GamePlay.Cards;
using ArcardnoidContent.Components.GameScene.Cards;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;

namespace ArcardnoidContent.Components.GameScene.Battle.Cards
{
    public class BattleCardsDeck : GameComponent
    {
        #region Private Properties

        private List<CardImage> CardImages { get; set; }
        private IGamePlay GamePlay { get; set; } = GameServiceProvider.GetService<IGamePlay>();

        #endregion Private Properties

        #region Public Constructors

        public BattleCardsDeck(int x, int y) : base(x, y)
        {
            GamePlay.CardChanged += GamePlay_CardChanged;
            CardImages = new List<CardImage>();
            List<Card> cards = GamePlay.GetCards();
            int innerY = 200;
            foreach (var card in cards)
            {
                var cardImage = new CardImage(card, 130, innerY, true);
                CardImages.Add(cardImage);
                AddGameComponent(cardImage);
                innerY += 300;
            }
        }

        #endregion Public Constructors

        #region Private Methods

        private void GamePlay_CardChanged(List<Card> cards)
        {
        }

        #endregion Private Methods
    }
}