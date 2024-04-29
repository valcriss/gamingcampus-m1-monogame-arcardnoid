using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models.Content.Components.Map
{
    public class MapChunk
    {
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("level")]
        public int Level { get; set; }
        [JsonProperty("layers")]
        public List<MapLayer> Layers { get; set; }
        [JsonProperty("blocks")]
        public MapLayer Blocks { get; set; }
        [JsonProperty("entrances")]
        public List<MapChunkEntrance> Entrances { get; set;} = new List<MapChunkEntrance>();
    }
}
