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
            List<Card> cards = GamePlay.GetCards().Where(c => c.InBattle).ToList();
            int innerY = 64;
            for (int i = 0; i < cards.Count; i++)
            {
                Card? card = cards[i];
                var cardImage = new CardImage(card, i<3 ? 30: 1700, innerY, true, CardClicked);
                CardImages.Add(cardImage);
                AddGameComponent(cardImage);
                innerY += 310;
                if(i == 2)
                {
                    innerY = 64;
                }
            }
        }

        #endregion Public Constructors

        #region Private Methods

        private void CardClicked(CardImage card)
        {
            switch (card.Card.CardType)
            {
                case CardType.Speed:
                    GamePlay.ChangePlayerSpeed(card.Card.CardParam, 30);
                    break;
                case CardType.AttackSpell:
                    GamePlay.CastAttackSpell(card.Card);
                    break;
            }
            if (!card.Card.Reusable)
            {
                GamePlay.RemoveCard(card.Card);
                card.InnerUnload();
            }
        }

        private void GamePlay_CardChanged(List<Card> cards)
        {
        }

        #endregion Private Methods
    }
}