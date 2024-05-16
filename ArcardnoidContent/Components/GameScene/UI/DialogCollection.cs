using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class DialogCollection
    {
        [JsonProperty("dialogs")]
        public List<EncounterDialogCollection> EncounterDialogs { get; set; } = new List<EncounterDialogCollection>();
    }
}
