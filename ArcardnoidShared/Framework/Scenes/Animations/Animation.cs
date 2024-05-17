using ArcardnoidShared.Framework.Scenes.Components;

namespace ArcardnoidShared.Framework.Scenes.Animations
{
    public class Animation
    {
        #region Public Properties

        public bool Loop { get; set; }
        public bool PlayOnStart { get; set; }
        public AnimationState State { get; private set; } = AnimationState.Waiting;

        #endregion Public Properties

        #region Protected Properties

        protected GameComponent? Component { get; set; }
        protected float Duration { get; set; }
        protected EaseType Ease { get; set; } = EaseType.Linear;
        protected float ElapsedTime { get; set; }
        protected Action? OnAnimationCompleted { get; set; }
        protected float Ratio => EasingFunctions.GetEase(Ease, ElapsedTime / Duration);

        #endregion Protected Properties

        #region Public Constructors

        public Animation(float duration, bool loop = false, bool playOnStart = false, EaseType ease = EaseType.Linear, Action? onAnimationCompleted = null)
        {
            Duration = duration;
            Loop = loop;
            PlayOnStart = playOnStart;
            Ease = ease;
            OnAnimationCompleted = onAnimationCompleted;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Play()
        {
            State = AnimationState.Playing;
            ElapsedTime = 0;
        }

        public void SetComponent(GameComponent component)
        {
            Component = component;
        }

        public void Stop()
        {
            State = Loop ? AnimationState.Playing : AnimationState.Ended;
            if (State == AnimationState.Ended)
            {
                OnAnimationCompleted?.Invoke();
            }
            ElapsedTime = 0;
        }

        public virtual void Update(float delta)
        {
            if (State == AnimationState.Waiting && PlayOnStart)
            {
                Play();
            }
            if (State == AnimationState.Playing)
            {
                ElapsedTime += delta;
                if (ElapsedTime >= Duration)
                {
                    Stop();
                }
            }
        }

        #endregion Public Methods
    }
}