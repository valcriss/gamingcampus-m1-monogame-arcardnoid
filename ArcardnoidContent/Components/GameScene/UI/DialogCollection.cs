using Newtonsoft.Json;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class DialogCollection
    {
        #region Public Properties

        [JsonProperty("dialogs")]
        public List<EncounterDialogCollection> EncounterDialogs { get; set; } = new List<EncounterDialogCollection>();

        #endregion Public Properties
    }
}