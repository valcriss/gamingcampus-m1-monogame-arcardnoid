using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models.Content.Components.Map.Models
{
    public class MapBridgePosition
    {
        public MapChunkDoorType DoorType { get; set; }
        public int FromX { get; set; }
        public int FromY { get; set; }

        public int ToX { get; set; }
        public int ToY { get; set; }
    }
}
