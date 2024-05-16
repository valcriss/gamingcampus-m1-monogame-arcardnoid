using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidContent.Components.Shared.Map.Models
{
    public struct MapChunkPosition
    {
        #region Public Properties

        public MapChunkDoorType DoorType { get; internal set; }
        public MapChunkDoor From { get; set; }
        public MapChunk MapChunk { get; set; }
        public MapChunkEntrance To { get; internal set; }
        public int X { get; set; }
        public int Y { get; set; }

        #endregion Public Properties
    }
}
