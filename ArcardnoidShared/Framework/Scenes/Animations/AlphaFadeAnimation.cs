﻿using ArcardnoidShared.Framework.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidShared.Framework.Scenes.Animations
{
    public class AlphaFadeAnimation : Animation
    {
        #region Private Fields

        private float _from;
        private float _to;

        #endregion Private Fields

        #region Public Constructors

        public AlphaFadeAnimation(float duration, float from, float to, bool loop = false, bool playOnStart = false, EaseType easeType = EaseType.Linear, Action? onAnimationCompleted = null) : base(duration, loop, playOnStart, easeType, onAnimationCompleted)
        {
            _from = from;
            _to = to;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Update(float delta)
        {
            if (Component == null) return;

            base.Update(delta);
            if (State == AnimationState.Playing)
            {
                Component.Opacity = MathTools.Lerp(_from, _to, Ratio);
            }
        }

        #endregion Public Methods
    }
}
