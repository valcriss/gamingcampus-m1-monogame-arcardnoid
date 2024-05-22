using ArcardnoidShared.Framework.Drawing;

namespace ArcardnoidContent.Components.Shared.Map.Models
{
    public struct MapChunkDoor
    {
        #region Public Properties

        public MapChunkDoorType DoorType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        #endregion Public Properties

        #region Public Constructors

        public MapChunkDoor(MapChunkDoorType doorType, int x, int y)
        {
            DoorType = doorType;
            X = x;
            Y = y;
        }

        #endregion Public Constructors

        #region Public Methods

        public readonly Point GetOppositeChunkPosition(MapChunkDoorType doorType, MapChunkEntrance possibleEntrance)
        {
            return doorType switch
            {
                MapChunkDoorType.Top => new Point(X - possibleEntrance.X, Y - possibleEntrance.Y - 2),
                MapChunkDoorType.Bottom => new Point(X - possibleEntrance.X, Y + possibleEntrance.Y + 2),
                MapChunkDoorType.Right => new Point(X + possibleEntrance.X + 2, Y - possibleEntrance.Y),
                MapChunkDoorType.Left => new Point(X - possibleEntrance.X - 2, Y - possibleEntrance.Y),
                _ => Point.Zero,
            };
        }

        public readonly List<MapChunkDoorType> GetOppositeDoorTypes()
        {
            return DoorType switch
            {
                MapChunkDoorType.Top => new List<MapChunkDoorType> { MapChunkDoorType.Bottom, MapChunkDoorType.BottomRight, MapChunkDoorType.BottomLeft },
                MapChunkDoorType.Right => new List<MapChunkDoorType> { MapChunkDoorType.Left, MapChunkDoorType.TopLeft, MapChunkDoorType.BottomLeft },
                MapChunkDoorType.Bottom => new List<MapChunkDoorType> { MapChunkDoorType.Top, MapChunkDoorType.TopRight, MapChunkDoorType.TopLeft },
                MapChunkDoorType.Left => new List<MapChunkDoorType> { MapChunkDoorType.Right, MapChunkDoorType.TopRight, MapChunkDoorType.BottomRight },
                MapChunkDoorType.TopRight => new List<MapChunkDoorType> { MapChunkDoorType.Bottom, MapChunkDoorType.BottomRight, MapChunkDoorType.BottomLeft, MapChunkDoorType.Left, MapChunkDoorType.TopLeft },
                MapChunkDoorType.TopLeft => new List<MapChunkDoorType> { MapChunkDoorType.Bottom, MapChunkDoorType.BottomRight, MapChunkDoorType.BottomLeft, MapChunkDoorType.Right, MapChunkDoorType.TopRight },
                MapChunkDoorType.BottomRight => new List<MapChunkDoorType> { MapChunkDoorType.Top, MapChunkDoorType.TopRight, MapChunkDoorType.TopLeft, MapChunkDoorType.Left, MapChunkDoorType.BottomLeft },
                MapChunkDoorType.BottomLeft => new List<MapChunkDoorType> { MapChunkDoorType.Left, MapChunkDoorType.TopLeft, MapChunkDoorType.BottomLeft, MapChunkDoorType.Top, MapChunkDoorType.TopRight },
                _ => new List<MapChunkDoorType>(),
            };
        }

        #endregion Public Methods
    }
}