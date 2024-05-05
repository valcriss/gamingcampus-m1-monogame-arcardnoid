using Newtonsoft.Json;

namespace arcardnoid.Models.Content.Components.Map.Models
{
    public struct MapLayer
    {
        #region Public Properties

        [JsonProperty("data")]
        public string[] Data { get; set; } = new string[0];

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        #endregion Public Properties

        #region Public Constructors

        public MapLayer()
        {
        }

        #endregion Public Constructors
    }
}