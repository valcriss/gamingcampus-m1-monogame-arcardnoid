using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidContent.Components.Shared.Map.Models
{
    public struct ChunkLayout
    {
        #region Public Properties

        public MapChunk MapChunk { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        #endregion
    }
}
