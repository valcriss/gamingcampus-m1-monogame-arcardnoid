using arcardnoid.Models.Framework.Tools;
using ArcardnoidShared.Framework.Scenes.Components;
using ArcardnoidShared.Framework.ServiceProvider;

namespace ArcardnoidShared.Framework.Components.Images
{
    public class Primitive2D : GameComponent
    {
        #region Private Properties

        private static IPrimitives2D Primitives2D => GameServiceProvider.GetService<IPrimitives2D>();

        #endregion Private Properties

        #region Private Fields

        private Action<IPrimitives2D>? _drawAction = null;

        #endregion Private Fields

        #region Public Constructors

        public Primitive2D(Action<IPrimitives2D>? drawAction = null)
        {
            _drawAction = drawAction;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Draw()
        {
            base.Draw();
            _drawAction?.Invoke(Primitives2D);
        }

        public Primitive2D SetDrawAction(Action<IPrimitives2D>? drawAction)
        {
            _drawAction = drawAction;
            return this;
        }

        #endregion Public Methods
    }
}