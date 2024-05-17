using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using Microsoft.Xna.Framework.Input;

namespace arcardnoid.Models.Services
{
    public class MouseService : IMouseService
    {
        #region Private Properties

        private MouseState PreviousMouseState { get; set; }
        private MouseState State { get; set; }

        #endregion Private Properties

        #region Public Methods

        public Point GetMousePosition()
        {
            return new Point(State.Position.X, State.Position.Y);
        }

        public bool IsMouseLeftButtonPressed()
        {
            return State.LeftButton == ButtonState.Pressed && PreviousMouseState.LeftButton == ButtonState.Released;
        }

        public void Update()
        {
            PreviousMouseState = State;
            State = Mouse.GetState();
        }

        #endregion Public Methods
    }
}