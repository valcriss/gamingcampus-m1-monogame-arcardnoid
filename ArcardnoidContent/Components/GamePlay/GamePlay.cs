using ArcardnoidContent.Components.GamePlay.Cards;

namespace ArcardnoidContent.Components.GamePlay
{
    public class GamePlay : IGamePlay
    {
        #region Public Events

        public event Action<List<Card>>? CardChanged;

        public event Action<int>? GoldChanged;

        public event Action<int>? HeartChanged;

        public event Action<float, float>? OponentSpeedChanged;

        public event Action<float, float>? PlayerSpeedChanged;

        public event Action<int>? UnitsChanged;
        public event Action<Card>? AttackSpellCasted;

        #endregion Public Events

        #region Private Fields

        private const int MAXIMUM_HEART = 2;
        private const int MAXIMUM_UNITS = 5 * 20;
        private readonly List<Card> _cards = new List<Card>();
        private int _gold = 200;
        private int _heart = MAXIMUM_HEART;
        private float _opponentSpeed = 1;
        private float _opponentSpeedDuration = 0;
        private float _playerSpeed = 1;
        private float _playerSpeedDuration = 0;
        private int _units = MAXIMUM_UNITS;

        #endregion Private Fields

        #region Public Methods

        public void AddCard(Card card)
        {
            _cards.Add(card);
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

        public void ChangeOponentSpeed(float speed, float duration)
        {
            _opponentSpeed = speed;
            _opponentSpeedDuration = duration;
            OponentSpeedChanged?.Invoke(_opponentSpeed, duration);
        }

        public void ChangePlayerSpeed(float speed, float duration)
        {
            _playerSpeed = speed;
            _playerSpeedDuration = duration;
            PlayerSpeedChanged?.Invoke(_playerSpeed, duration);
        }

        public List<Card> GetCards()
        {
            return _cards;
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

        public float GetSpeed()
        {
            return _playerSpeed;
        }

        public int GetUnits()
        {
            return _units;
        }

        public void RemoveCard(Card card)
        {
            _cards.Remove(card);
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

        public void Update(float delta)
        {
            if (_playerSpeedDuration > 0)
            {
                _playerSpeedDuration -= delta;
                if (_playerSpeedDuration <= 0)
                {
                    _playerSpeed = 1;
                    _playerSpeedDuration = 0;
                    PlayerSpeedChanged?.Invoke(_playerSpeed, 0);
                }
            }
            if (_opponentSpeedDuration > 0)
            {
                _opponentSpeedDuration -= delta;
                if (_opponentSpeedDuration <= 0)
                {
                    _opponentSpeed = 1;
                    _opponentSpeedDuration = 0;
                    OponentSpeedChanged?.Invoke(_opponentSpeed, 0);
                }
            }
        }

        public void UseCard(Card card)
        {
        }

        public void CastAttackSpell(Card card)
        {
            AttackSpellCasted?.Invoke(card);
        }

        #endregion Public Methods
    }
}