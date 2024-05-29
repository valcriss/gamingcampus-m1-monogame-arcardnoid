using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using Newtonsoft.Json;
using System.Reflection;

namespace ArcardnoidContent.Components.GamePlay.Cards
{
    public class CardProviderService : ICardProviderService
    {
        #region Private Properties

        private IGamePlay GamePlay { get; set; } = GameServiceProvider.GetService<IGamePlay>();

        #endregion Private Properties

        #region Private Fields

        private List<Card>? _cards = null;

        #endregion Private Fields

        #region Public Methods

        public Card GetRandomCard(IRandom random, bool filterFree)
        {
            InitializeCards();
            if (_cards == null)
            {
                throw new Exception("Cards not initialized");
            }
            Card[] cards = (filterFree) ? _cards.Where(c => c.CardCost > 0).ToArray() : _cards.ToArray();
            Card? selected = null;
            while (selected == null)
            {
                int index = random.Next(0, cards.Length - 1);
                selected = cards[index];
                if (GamePlay.GetCards().Contains(selected))
                {
                    selected = null;
                }
            }
            return selected;
        }

        #endregion Public Methods

        #region Private Methods

        private void InitializeCards()
        {
            if (_cards == null)
            {
                string location = Assembly.GetExecutingAssembly().Location;
                string? directory = Path.GetDirectoryName(location) ?? throw new Exception("Directory not found");
                string cardsFilename = File.ReadAllText(Path.Combine(directory, @"Cards/cards.json"));
                this._cards = JsonConvert.DeserializeObject<List<Card>>(cardsFilename);
            }
        }

        #endregion Private Methods
    }
}