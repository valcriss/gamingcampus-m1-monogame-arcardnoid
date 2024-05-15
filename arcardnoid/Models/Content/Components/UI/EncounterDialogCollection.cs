using arcardnoid.Models.Content.Components.Map.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models.Content.Components.UI
{
    public class EncounterDialogCollection
    {
        [JsonProperty("type")]
        public EncounterType Type { get; set; } = EncounterType.None;

        [JsonProperty("dialogs")]
        public List<EncounterDialog> EncounterDialogs { get; set; } = new List<EncounterDialog>();
    }
}
