using Newtonsoft.Json;

namespace arcardnoid.Models.Content.Components.Map.Models
{
    public class MapChunkSpawn
    {
        #region Public Properties

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }

        #endregion Public Properties
    }
}