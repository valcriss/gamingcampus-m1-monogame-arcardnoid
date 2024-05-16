using ArcardnoidShared.Framework.Scenes.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidShared.Framework.Components.Tools
{
    public class TimeOutAction : GameComponent
    {
        private float _duration;
        private float _time;
        private bool _loop;
        private Action _onComplete;
        public TimeOutAction(float duration, Action onComplete, bool loop = false) : base()
        {
            _duration = duration;
            _onComplete = onComplete;
            _loop = loop;
        }

        public override void Update(float delta)
        {
            _time += delta;
            if (_time > _duration)
            {
                _onComplete?.Invoke();
                if(_loop)
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
    }
}
