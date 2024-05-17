using ArcardnoidContent.Components.GamePlay.Cards;

namespace ArcardnoidContent.Components.GamePlay
{
    public class GamePlay : IGamePlay
    {
        #region Public Events

        public event Action<List<Card>>? CardChanged;

        public event Action<int>? GoldChanged;

        public event Action<int>? HeartChanged;

        #endregion Public Events

        #region Private Fields

        private int _gold = 200;
        private int _heart = 2;

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
            _heart = Math.Min(2, _heart + heart);
            HeartChanged?.Invoke(_heart);
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

        public void UseCard(Card card)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}