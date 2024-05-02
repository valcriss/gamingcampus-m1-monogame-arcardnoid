using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace arcardnoid.Models.Content.Components.Map.Models
{
    public class MapChunk
    {
        #region Public Properties

        [JsonProperty("blocks")]
        public MapLayer Blocks { get; set; }

        [JsonProperty("entrances")]
        public List<MapChunkEntrance> Entrances { get; set; } = new List<MapChunkEntrance>();

        [JsonProperty("spawns")]
        public List<MapChunkSpawn> Spawns { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("layers")]
        public List<MapLayer> Layers { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        public static MapChunk FromFile(string file)
        {
            return JsonConvert.DeserializeObject<MapChunk>(File.ReadAllText(file));
        }

        #endregion Public Properties
    }
}