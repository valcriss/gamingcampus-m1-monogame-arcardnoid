using Newtonsoft.Json;

namespace arcardnoid.Models.Content.Components.Map.Models
{
    public class MapLayer
    {
        #region Public Properties

        [JsonProperty("data")]
        public string[] Data { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        #endregion Public Properties
    }
}