namespace ArcardnoidContent.Components.GamePlay.Cards
{
    public class Card
    {
        #region Public Properties

        public int CardCost { get; set; }
        public string? CardDescription { get; set; }
        public int CardId { get; set; }
        public string? CardName { get; set; }
        public float CardParam { get; set; }
        public CardRarity CardRarity { get; set; } = CardRarity.None;
        public CardType CardType { get; set; } = CardType.None;
        public bool Reusable { get; set; }

        #endregion Public Properties
    }
}