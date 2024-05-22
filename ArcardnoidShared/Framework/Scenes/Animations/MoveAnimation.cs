using ArcardnoidShared.Framework.Drawing;
using ArcardnoidShared.Framework.Tools;

namespace ArcardnoidShared.Framework.Scenes.Animations
{
    public class MoveAnimation : Animation
    {
        #region Private Fields

        private readonly Point _from;
        private readonly Point _to;

        #endregion Private Fields

        #region Public Constructors

        public MoveAnimation(float duration, Point from, Point to, bool loop = false, bool playOnStart = false, EaseType easeType = EaseType.Linear, Action? onAnimationCompleted = null, float startAfter = 0) : base(duration, loop, playOnStart, easeType, onAnimationCompleted, startAfter)
        {
            _from = from;
            _to = to;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Stop()
        {
            base.Stop();
            if (Component == null) return;
            Component.Position = _to;
        }

        public override void Update(float delta)
        {
            base.Update(delta);
            if (Component == null) return;
            if (!CanStart)
            {
                Component.Position = _from;
            }
            else if (State == AnimationState.Playing)
            {
                float x = MathF.Abs(_from.X - _to.X) > 0.01 ? MathTools.Lerp(_from.X, _to.X, Ratio) : _to.X;
                float y = MathF.Abs(_from.Y - _to.Y) > 0.01 ? MathTools.Lerp(_from.Y, _to.Y, Ratio) : _to.Y;
                Point newPosition = new(x, y);
                Component.Visible = true;
                Component.Position = newPosition;
            }
        }

        #endregion Public Methods
    }
}