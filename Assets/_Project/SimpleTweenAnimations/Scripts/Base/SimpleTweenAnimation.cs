using _Project.StrangeIOCUtility;
using System;
using UnityEngine;

namespace _Project.SimpleTweenAnimations.Scripts.Base
{
    public abstract class SimpleTweenAnimation : ViewZeitnot
    {
        [NonSerialized]
        public Action<SimpleTweenAnimation> OnAnimationStartEvent = delegate { };
        [NonSerialized]
        public Action<SimpleTweenAnimation> OnAnimationCompleteEvent = delegate { };

        [field: SerializeField] protected string AnimationName { get; private set; }
        [field: SerializeField] protected bool PlayOnEnable { get; private set; }

        protected override void OnDestroy()
        {
            OnAnimationStartEvent = null;
            OnAnimationCompleteEvent = null;
            base.OnDestroy();
        }

        public abstract void Play(bool killExisting = false);
        public abstract void Stop();

        protected abstract void CreateAnimation(bool killExisting);
        protected abstract void KillAnimation();

    }
}