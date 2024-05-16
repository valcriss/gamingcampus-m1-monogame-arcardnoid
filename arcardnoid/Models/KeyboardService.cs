using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models
{
    public class KeyboardService : IKeyboardService
    {
        private KeyboardState PreviousKeyState { get; set; }

        private KeyboardState State { get; set; } 
        public void Update()
        {
            PreviousKeyState = State;
            State = Keyboard.GetState();
        }
        public bool IsKeyDown(string key)
        {
            return State.IsKeyDown((Keys)Enum.Parse(typeof(Keys), key));
        }

        public bool HasBeenPressed(string k)
        {
            Keys key = (Keys)Enum.Parse(typeof(Keys), k);
            return State.IsKeyDown(key) && !PreviousKeyState.IsKeyDown(key);
        }
    }
}
