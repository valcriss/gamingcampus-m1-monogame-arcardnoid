using Newtonsoft.Json;

namespace ArcardnoidContent.Components.Shared.Map.Models
{
    public struct MapChunkSpawn
    {
        #region Public Properties

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }

        #endregion Public Properties
    }
}