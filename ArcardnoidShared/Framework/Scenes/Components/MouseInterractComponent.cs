using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.ServiceProvider;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using ArcardnoidShared.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidShared.Framework.Scenes.Components
{
    public class MouseInterractComponent : GameComponent
    {
        protected MouseInterractState InterractState { get; set; } = MouseInterractState.Normal;
        protected Action? OnClick { get; set; } = null;
        private bool WasPressed { get; set; } = false;

        public MouseInterractComponent(Action? onClick = null, int x = 0, int y = 0, int width = 0, int height = 0, float rotation = 0, float scale = 1) : base(x, y, width, height, rotation, scale)
        {
            OnClick = onClick;
        }

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
    }
}
