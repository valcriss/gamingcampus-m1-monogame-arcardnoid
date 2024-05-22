using ArcardnoidShared.Framework.Scenes.Components;

namespace ArcardnoidShared.Framework.Components.Tools
{
    public class TimeOutAction : GameComponent
    {
        #region Private Fields

        private readonly float _duration;
        private readonly bool _loop;
        private readonly Action _onComplete;
        private float _time;

        #endregion Private Fields

        #region Public Constructors

        public TimeOutAction(float duration, Action onComplete, bool loop = false) : base()
        {
            _duration = duration;
            _onComplete = onComplete;
            _loop = loop;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Update(float delta)
        {
            _time += delta;
            if (_time > _duration)
            {
                _onComplete?.Invoke();
                if (_loop)
                {
                    _time = 0;
                }
                else
                {
                    InnerUnload();
                }
            }
            base.Update(delta);
        }

        #endregion Public Methods
    }
}