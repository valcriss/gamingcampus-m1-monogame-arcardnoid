using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class EncounterDialog
    {
        [JsonProperty("steps")]
        public List<DialogStep> Steps { get; set; } = new List<DialogStep>();

        [JsonProperty("gold")]
        public int Gold { get; set; } = 0;
        [JsonProperty("heart")]
        public int Heart { get; set; } = 0;
    }
}
