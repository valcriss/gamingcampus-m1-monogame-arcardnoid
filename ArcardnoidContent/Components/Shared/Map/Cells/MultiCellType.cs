using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidContent.Components.Shared.Map.Cells
{
    public enum MultiCellType
    {
        None,
        TopLeft,
        Top,
        TopRight,
        Left,
        Center,
        Right,
        BottomLeft,
        Bottom,
        BottomRight,
        TopLeftBottom,
        TopBottom,
        TopRightBottom,
        LeftRightTop,
        LeftRight,
        LeftRightBottom,
        Single
    }
}