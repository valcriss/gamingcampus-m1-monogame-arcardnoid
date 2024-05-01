using Newtonsoft.Json;

namespace arcardnoid.Models.Content.Components.Map.Models
{
    public class MapChunkEntrance
    {
        #region Public Properties

        [JsonProperty("from")]
        public int From { get; set; }

        [JsonProperty("to")]
        public int To { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }

        #endregion Public Properties
    }
}