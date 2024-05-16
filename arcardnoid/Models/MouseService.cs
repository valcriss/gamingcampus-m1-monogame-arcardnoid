using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models
{
    public class MouseService : IMouseService
    {
        private MouseState State { get; set; }

        public void Update()
        {
            State = Mouse.GetState();
        }

        public Point GetMousePosition()
        {
            return new Point(State.Position.X, State.Position.Y);
        }

        public bool IsMouseLeftButtonPressed()
        {
            return State.LeftButton == ButtonState.Pressed;
        }
    }
}
