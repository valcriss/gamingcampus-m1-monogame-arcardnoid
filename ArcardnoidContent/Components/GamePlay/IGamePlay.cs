using ArcardnoidContent.Components.GamePlay.Cards;

namespace ArcardnoidContent.Components.GamePlay
{
    public interface IGamePlay
    {
        #region Public Events

        event Action<List<Card>>? CardChanged;

        event Action<int>? GoldChanged;

        event Action<int>? HeartChanged;

        event Action<int>? UnitsChanged;

        #endregion Public Events

        #region Public Methods

        void AddUnits(int units);

        void AddCard(Card card);

        void AddGold(int gold);

        void AddHeart(int heart);

        bool CanBuy(int gold);

        List<Card> GetCards();

        int GetGold();

        int GetHeart();

        int GetUnits();

        int GetMaxUnits();

        void RemoveCard(Card card);

        void RemoveGold(int gold);

        void RemoveHeart(int heart);

        void RemoveUnits(int units);

        void UseCard(Card card);

        #endregion Public Methods
    }
}