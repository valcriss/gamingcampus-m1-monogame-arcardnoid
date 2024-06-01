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

        [JsonProperty("smalltexture")]
        public TextureType CardSmallTexture { get; set; }

        [JsonProperty("texture")]
        public TextureType CardTexture { get; set; }

        [JsonProperty("spelltexture")]
        public TextureType? SpellTexture { get; set; } = null;

        [JsonProperty("spellrows")]
        public int SpellRows { get; set; } = 0;

        [JsonProperty("spellcols")]
        public int SpellCols { get; set; } = 0;

        [JsonProperty("spellspeed")]
        public float SpellSpeed { get; set; } = 0;

        [JsonProperty("type")]
        public CardType CardType { get; set; } = CardType.None;

        [JsonProperty("inBattle")]
        public bool InBattle { get; set; }

        [JsonProperty("maxUnits")]
        public int MaxUnits { get; set; } = 0;

        [JsonProperty("reusable")]
        public bool Reusable { get; set; }

        #endregion Public Properties
    }
}