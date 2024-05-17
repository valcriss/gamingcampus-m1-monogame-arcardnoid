using ArcardnoidContent.Components.GamePlay.Cards;

namespace ArcardnoidContent.Components.GamePlay
{
    public interface IGamePlay
    {
        #region Public Events

        event Action<List<Card>>? CardChanged;

        event Action<int>? GoldChanged;

        event Action<int>? HeartChanged;

        #endregion Public Events

        #region Public Methods

        void AddCard(Card card);

        void AddGold(int gold);

        void AddHeart(int heart);

        bool CanBuy(int gold);

        List<Card> GetCards();

        int GetGold();

        int GetHeart();

        void RemoveCard(Card card);

        void RemoveGold(int gold);

        void RemoveHeart(int heart);

        void UseCard(Card card);

        #endregion Public Methods
    }
}