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

        void AddCard(Card card);

        void AddGold(int gold);

        void AddHeart(int heart);

        void AddUnits(int units);

        bool CanBuy(int gold);

        List<Card> GetCards();

        int GetGold();

        int GetHeart();

        int GetMaxUnits();

        int GetUnits();

        void RemoveCard(Card card);

        void RemoveGold(int gold);

        void RemoveHeart(int heart);

        void RemoveUnits(int units);

        void UseCard(Card card);

        #endregion Public Methods
    }
}