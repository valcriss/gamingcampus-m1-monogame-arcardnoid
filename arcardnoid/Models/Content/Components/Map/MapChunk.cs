using Newtonsoft.Json;
using System.Collections.Generic;

namespace arcardnoid.Models.Content.Components.Map
{
    public class MapChunk
    {
        #region Public Properties

        [JsonProperty("blocks")]
        public MapLayer Blocks { get; set; }

        [JsonProperty("entrances")]
        public List<MapChunkEntrance> Entrances { get; set; } = new List<MapChunkEntrance>();

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("layers")]
        public List<MapLayer> Layers { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        #endregion Public Properties
    }
}