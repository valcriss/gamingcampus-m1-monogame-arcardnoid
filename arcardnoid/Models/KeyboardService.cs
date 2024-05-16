using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using Microsoft.Xna.Framework.Input;
using System;

namespace arcardnoid.Models
{
    public class KeyboardService : IKeyboardService
    {
        #region Private Properties

        private KeyboardState PreviousKeyState { get; set; }

        private KeyboardState State { get; set; }

        #endregion Private Properties

        #region Public Methods

        public bool HasBeenPressed(string k)
        {
            Keys key = (Keys)Enum.Parse(typeof(Keys), k);
            return State.IsKeyDown(key) && !PreviousKeyState.IsKeyDown(key);
        }

        public bool IsKeyDown(string key)
        {
            return State.IsKeyDown((Keys)Enum.Parse(typeof(Keys), key));
        }

        public void Update()
        {
            PreviousKeyState = State;
            State = Keyboard.GetState();
        }

        #endregion Public Methods
    }
}