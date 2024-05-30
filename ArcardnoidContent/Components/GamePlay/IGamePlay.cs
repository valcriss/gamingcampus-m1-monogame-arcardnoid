using ArcardnoidContent.Components.GamePlay.Cards;

namespace ArcardnoidContent.Components.GamePlay
{
    public interface IGamePlay
    {
        #region Public Events

        event Action<List<Card>>? CardChanged;

        event Action<int>? GoldChanged;

        event Action<int>? HeartChanged;

        event Action<float, float>? OponentSpeedChanged;

        event Action<float, float>? PlayerSpeedChanged;

        event Action<int>? UnitsChanged;

        #endregion Public Events

        #region Public Methods

        void AddCard(Card card);

        void AddGold(int gold);

        void AddHeart(int heart);

        void AddUnits(int units);

        bool CanBuy(int gold);

        void ChangeOponentSpeed(float speed, float duration);

        void ChangePlayerSpeed(float speed, float duration);

        List<Card> GetCards();

        int GetGold();

        int GetHeart();

        int GetMaxUnits();

        float GetSpeed();

        int GetUnits();

        void RemoveCard(Card card);

        void RemoveGold(int gold);

        void RemoveHeart(int heart);

        void RemoveUnits(int units);

        void Update(float delta);

        void UseCard(Card card);

        #endregion Public Methods
    }
}