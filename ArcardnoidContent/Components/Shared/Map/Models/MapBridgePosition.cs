namespace ArcardnoidContent.Components.Shared.Map.Models
{
    public class MapBridgePosition
    {
        #region Public Properties

        public MapChunkDoorType DoorType { get; set; }
        public int FromX { get; set; }
        public int FromY { get; set; }

        public int ToX { get; set; }
        public int ToY { get; set; }

        #endregion Public Properties
    }
}