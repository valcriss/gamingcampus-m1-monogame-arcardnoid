using ArcardnoidContent.Components.GamePlay.Cards;

namespace ArcardnoidContent.Components.GamePlay
{
    public class GamePlay : IGamePlay
    {
        #region Public Events

        public event Action<List<Card>>? CardChanged;

        public event Action<int>? GoldChanged;

        public event Action<int>? HeartChanged;

        public event Action<int>? UnitsChanged;

        #endregion Public Events

        #region Private Fields

        private const int MAXIMUM_HEART = 2;
        private const int MAXIMUM_UNITS = 5 * 20;
        private int _gold = 200;
        private int _heart = MAXIMUM_HEART;
        private int _units = MAXIMUM_UNITS;

        #endregion Private Fields

        #region Public Methods

        public void AddCard(Card card)
        {
            throw new NotImplementedException();
        }

        public void AddGold(int gold)
        {
            _gold += gold;
            GoldChanged?.Invoke(_gold);
        }

        public void AddHeart(int heart)
        {
            _heart = Math.Min(MAXIMUM_HEART, _heart + heart);
            HeartChanged?.Invoke(_heart);
        }

        public void AddUnits(int units)
        {
            _units += Math.Min(MAXIMUM_UNITS, units);
        }

        public bool CanBuy(int gold)
        {
            return _gold >= gold;
        }

        public List<Card> GetCards()
        {
            throw new NotImplementedException();
        }

        public int GetGold()
        {
            return _gold;
        }

        public int GetHeart()
        {
            return _heart;
        }

        public int GetMaxUnits()
        {
            return MAXIMUM_UNITS;
        }

        public int GetUnits()
        {
            return _units;
        }

        public void RemoveCard(Card card)
        {
            throw new NotImplementedException();
        }

        public void RemoveGold(int gold)
        {
            _gold = Math.Max(0, _gold - gold);
            GoldChanged?.Invoke(_gold);
        }

        public void RemoveHeart(int heart)
        {
            _heart = Math.Max(0, _heart - heart);
            HeartChanged?.Invoke(_heart);
        }

        public void RemoveUnits(int units)
        {
            _units = Math.Max(0, _units - units);
        }

        public void UseCard(Card card)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}