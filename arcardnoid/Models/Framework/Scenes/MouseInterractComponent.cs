using arcardnoid.Models.Framework.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace arcardnoid.Models.Framework.Scenes
{
    public enum MouseInterractState
    {
        Normal,
        Hover,
        Pressed
    }

    public class MouseInterractComponent : Component
    {
        #region Protected Properties

        protected MouseInterractState InterractState { get; set; } = MouseInterractState.Normal;
        protected Action OnClick { get; set; }

        #endregion Protected Properties

        #region Private Properties

        private bool WasPressed { get; set; } = false;

        #endregion Private Properties

        #region Public Constructors

        public MouseInterractComponent(string name, Action onClick = null, int x = 0, int y = 0, int width = 0, int height = 0, float rotation = 0, float scale = 1) : base(name, x, y, width, height, rotation, scale)
        {
            OnClick = onClick;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MouseState state = Mouse.GetState();
            Point mousePoint = ScreenManager.UIScale(state.Position);
            if (RealBounds.Contains(mousePoint))
            {
                if (state.LeftButton == ButtonState.Pressed)
                {
                    InterractState = MouseInterractState.Pressed;
                    WasPressed = true;
                }
                else
                {
                    InterractState = MouseInterractState.Hover;
                    if (WasPressed)
                    {
                        OnClick?.Invoke();
                        WasPressed = false;
                    }
                }
            }
            else
            {
                InterractState = MouseInterractState.Normal;
                WasPressed = false;
            }
        }

        #endregion Public Methods
    }
}