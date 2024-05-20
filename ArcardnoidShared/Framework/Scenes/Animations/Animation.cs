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
        protected float StartAfter { get; set; }

        #endregion Protected Properties

        #region Protected Fields

        protected bool CanStart = true;

        #endregion Protected Fields

        #region Public Constructors

        public Animation(float duration, bool loop = false, bool playOnStart = false, EaseType ease = EaseType.Linear, Action? onAnimationCompleted = null, float startAfter = 0)
        {
            Duration = duration;
            Loop = loop;
            PlayOnStart = playOnStart;
            Ease = ease;
            OnAnimationCompleted = onAnimationCompleted;
            StartAfter = startAfter;
            CanStart = startAfter <= 0;
            ElapsedTime = 0;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual void Play()
        {
            State = AnimationState.Playing;
            ElapsedTime = 0;
        }

        public void SetComponent(GameComponent component)
        {
            Component = component;
        }

        public virtual void Stop()
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
            if (CanStart == false)
            {
                ElapsedTime += delta;
                if (ElapsedTime >= StartAfter)
                {
                    CanStart = true;
                    ElapsedTime = 0;
                }
            }
            else if (CanStart && State == AnimationState.Waiting && PlayOnStart)
            {
                Play();
            }
            else if (State == AnimationState.Playing)
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