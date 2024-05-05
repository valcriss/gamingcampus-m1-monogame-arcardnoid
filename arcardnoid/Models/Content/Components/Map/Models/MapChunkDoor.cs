using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace arcardnoid.Models.Content.Components.Map.Models
{
    public enum MapChunkDoorType
    {
        None,
        Top,
        Right,
        Bottom,
        Left,
        TopRight,
        TopLeft,
        BottomRight,
        BottomLeft
    }

    public class MapChunkDoor
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

        public List<MapChunkDoorType> GetOpositeDoorTypes()
        {
            switch (DoorType)
            {
                case MapChunkDoorType.Top:
                    return new List<MapChunkDoorType> { MapChunkDoorType.Bottom, MapChunkDoorType.BottomRight, MapChunkDoorType.BottomLeft };

                case MapChunkDoorType.Right:
                    return new List<MapChunkDoorType> { MapChunkDoorType.Left, MapChunkDoorType.TopLeft, MapChunkDoorType.BottomLeft };

                case MapChunkDoorType.Bottom:
                    return new List<MapChunkDoorType> { MapChunkDoorType.Top, MapChunkDoorType.TopRight, MapChunkDoorType.TopLeft };

                case MapChunkDoorType.Left:
                    return new List<MapChunkDoorType> { MapChunkDoorType.Right, MapChunkDoorType.TopRight, MapChunkDoorType.BottomRight };

                case MapChunkDoorType.TopRight:
                    return new List<MapChunkDoorType> { MapChunkDoorType.Bottom, MapChunkDoorType.BottomRight, MapChunkDoorType.BottomLeft, MapChunkDoorType.Left, MapChunkDoorType.TopLeft };

                case MapChunkDoorType.TopLeft:
                    return new List<MapChunkDoorType> { MapChunkDoorType.Bottom, MapChunkDoorType.BottomRight, MapChunkDoorType.BottomLeft, MapChunkDoorType.Right, MapChunkDoorType.TopRight };

                case MapChunkDoorType.BottomRight:
                    return new List<MapChunkDoorType> { MapChunkDoorType.Top, MapChunkDoorType.TopRight, MapChunkDoorType.TopLeft, MapChunkDoorType.Left, MapChunkDoorType.BottomLeft };

                case MapChunkDoorType.BottomLeft:
                    return new List<MapChunkDoorType> { MapChunkDoorType.Left, MapChunkDoorType.TopLeft, MapChunkDoorType.BottomLeft, MapChunkDoorType.Top, MapChunkDoorType.TopRight };

                default:
                    return new List<MapChunkDoorType>();
            }
        }

        public Point GetOpositeChunkPosition(MapChunkDoorType doorType, MapChunkEntrance possibleEntrance)
        {
            switch (doorType)
            {
                case MapChunkDoorType.Top:
                    return new Point(X - possibleEntrance.X, Y - possibleEntrance.Y - 2);
                case MapChunkDoorType.Bottom:
                    return new Point(X - possibleEntrance.X, Y + possibleEntrance.Y + 2);
                case MapChunkDoorType.Right:
                    return new Point(X + possibleEntrance.X + 2, Y - possibleEntrance.Y);
                case MapChunkDoorType.Left:
                    return new Point(X - possibleEntrance.X - 2 , Y - possibleEntrance.Y);
            }
            return Point.Zero;
        }

        #endregion Public Methods
    }
}