using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface IKeyboardService
    {
        void Update();
        bool IsKeyDown(string key);

        bool HasBeenPressed(string key);
    }
}
