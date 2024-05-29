using ArcardnoidShared.Framework.ServiceProvider.Enums;
using Newtonsoft.Json;

namespace ArcardnoidContent.Components.GamePlay.Cards
{
    public class Card
    {
        #region Public Properties

        [JsonProperty("cost")]
        public int CardCost { get; set; }

        [JsonProperty("id")]
        public string CardId { get; set; } = string.Empty;

        [JsonProperty("param")]
        public float CardParam { get; set; }

        [JsonProperty("rarity")]
        public CardRarity CardRarity { get; set; } = CardRarity.None;

        [JsonProperty("texture")]
        public TextureType CardTexture { get; set; }

        [JsonProperty("type")]
        public CardType CardType { get; set; } = CardType.None;

        [JsonProperty("reusable")]
        public bool Reusable { get; set; }

        #endregion Public Properties
    }
}