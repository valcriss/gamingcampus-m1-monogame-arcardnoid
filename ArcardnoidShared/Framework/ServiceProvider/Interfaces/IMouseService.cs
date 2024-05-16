using ArcardnoidShared.Framework.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface IMouseService
    {
        void Update();
        Point GetMousePosition();
        bool IsMouseLeftButtonPressed();
    }
}
