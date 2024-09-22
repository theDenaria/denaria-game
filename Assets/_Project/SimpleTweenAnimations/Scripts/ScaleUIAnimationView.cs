using _Project.SimpleTweenAnimations.Scripts.Base;
using DG.Tweening;
using System;
using UnityEngine;

namespace _Project.SimpleTweenAnimations.Scripts
{
    public class ScaleUIAnimationView : SimpleTweenAnimation
    {
        [field: SerializeField] private RectTransform AnimationNode { get; set; }
        [field: SerializeField] private Vector3 StartScale { get; set; } = Vector3.one;
        [field: SerializeField] private Vector3 EndScale { get; set; } = Vector3.one;
        [field: SerializeField] private float AnimationDuration { get; set; } = 1f;
        [field: SerializeField] private int LoopCount { get; set; } = 1;
        [field: SerializeField] private Ease Ease { get; set; } = Ease.Linear;

        private Tween AnimationTween { get; set; }

        private void OnEnable()
        {
            if (PlayOnEnable)
            {
                Play();
            }
        }

        public override void Play(bool killExisting = false)
        {
            CreateAnimation(killExisting);
        }

        public override void Stop()
        {
            KillAnimation();
        }

        protected override void CreateAnimation(bool killExisting)
        {
            if (killExisting)
            {
                KillAnimation();
            }
            else if (AnimationTween != null)
            {
                return;
            }

            AnimationNode.localScale = StartScale;
            AnimationTween = AnimationNode.DOScale(EndScale, AnimationDuration)
                .SetId(this)
                .SetLoops(LoopCount)
                .SetEase(Ease)
                .OnStart(() =>
                {
                    OnAnimationStartEvent.Invoke(this);
                })
                .OnComplete(() =>
                {
                    OnAnimationCompleteEvent(this);
                })
                .SetAutoKill(true)
                .OnKill(() =>
                {
                    AnimationTween = null;
                });
        }

        protected override void KillAnimation()
        {
            if (AnimationTween != null)
            {
                if (AnimationTween.IsActive())
                {
                    AnimationTween.Kill(false);
                }
                AnimationTween = null;
            }
        }

    }
}