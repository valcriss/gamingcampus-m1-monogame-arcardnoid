using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidShared.Framework.Scenes.Components
{
    public class MouseInterractComponent : GameComponent
    {
        #region Protected Properties

        protected MouseInterractState InterractState { get; set; } = MouseInterractState.Normal;
        protected Action? OnClick { get; set; } = null;

        #endregion Protected Properties

        #region Private Properties

        private bool WasPressed { get; set; } = false;

        #endregion Private Properties

        #region Public Constructors

        public MouseInterractComponent(Action? onClick = null, int x = 0, int y = 0, int width = 0, int height = 0, float rotation = 0, float scale = 1) : base(x, y, width, height, rotation, scale)
        {
            OnClick = onClick;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Update(float delta)
        {
            base.Update(delta);
            IMouseService mouseService = GameServiceProvider.GetService<IMouseService>();
            mouseService.Update();
            Point mousePosition = mouseService.GetMousePosition();
            Point mousePoint = ScreenManager.UIScale(mousePosition);
            if (RealBounds.Contains(mousePoint))
            {
                if (mouseService.IsMouseLeftButtonPressed())
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