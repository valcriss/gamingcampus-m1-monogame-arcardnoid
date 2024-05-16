using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidContent.Components.GameScene.UI
{
    public class DialogStep
    {
        [JsonProperty("actor")]
        public string Actor { get; set; }

        [JsonProperty("face")]
        public string Face { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
