using ArcardnoidContent.Components.GameScene.UI;
using Newtonsoft.Json;

namespace ArcardnoidContent.Components.GameScene.Dialogs
{
    public class DialogCollection
    {
        #region Public Properties

        [JsonProperty("dialogs")]
        public List<EncounterDialogCollection> EncounterDialogs { get; set; } = new List<EncounterDialogCollection>();

        #endregion Public Properties
    }
}