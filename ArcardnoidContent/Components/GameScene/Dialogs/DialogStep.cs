using Newtonsoft.Json;

namespace ArcardnoidContent.Components.GameScene.Dialogs
{
    public class DialogStep
    {
        #region Public Properties

        [JsonProperty("actor")]
        public string? Actor { get; set; }

        [JsonProperty("face")]
        public string? Face { get; set; }

        [JsonProperty("text")]
        public string? Text { get; set; }

        #endregion Public Properties
    }
}