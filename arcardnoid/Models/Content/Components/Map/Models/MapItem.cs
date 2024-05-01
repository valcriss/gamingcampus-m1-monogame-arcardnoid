using Newtonsoft.Json;
using System.Collections.Generic;

namespace arcardnoid.Models.Content.Components.Map.Models
{
    public class MapItem
    {
        #region Public Properties

        [JsonProperty("assets")]
        public List<MapAsset> Assets { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("layers")]
        public List<MapLayer> Layers { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        #endregion Public Properties
    }
}