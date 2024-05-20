using ArcardnoidContent.Components.Shared.Map.Enums;
using Newtonsoft.Json;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class EncounterDialogCollection
    {
        #region Public Properties

        [JsonProperty("dialogs")]
        public List<EncounterDialog> EncounterDialogs { get; set; } = new List<EncounterDialog>();

        [JsonProperty("type")]
        public EncounterType Type { get; set; } = EncounterType.None;

        #endregion Public Properties
    }
}