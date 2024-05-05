using Newtonsoft.Json;

namespace arcardnoid.Models.Content.Components.Map.Models
{
    public class MapLayer
    {
        #region Public Properties

        [JsonProperty("data")]
        public string[] Data { get; set; } = new string[0];

        [JsonProperty("name")]
        public string Name { get; set; }

        #endregion Public Properties
    }
}