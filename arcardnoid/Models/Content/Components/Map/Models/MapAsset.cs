using Newtonsoft.Json;

namespace arcardnoid.Models.Content.Components.Map.Models
{
    public struct MapAsset
    {
        #region Public Properties

        [JsonProperty("columns")]
        public int Columns { get; set; }

        [JsonProperty("delayMax")]
        public int DelayMax { get; set; }

        [JsonProperty("delayMin")]
        public int DelayMin { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("offsetX")]
        public int OffsetX { get; set; }

        [JsonProperty("offsetY")]
        public int OffsetY { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("rows")]
        public int Rows { get; set; }

        [JsonProperty("speed")]
        public float Speed { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        #endregion Public Properties
    }
}