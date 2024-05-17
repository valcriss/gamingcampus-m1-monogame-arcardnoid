using ArcardnoidContent.Components.GameScene.Dialogs;
using Newtonsoft.Json;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class EncounterDialog
    {
        #region Public Properties

        [JsonProperty("gold")]
        public int Gold { get; set; } = 0;

        [JsonProperty("heart")]
        public int Heart { get; set; } = 0;

        [JsonProperty("steps")]
        public List<DialogStep> Steps { get; set; } = new List<DialogStep>();

        #endregion Public Properties
    }
}