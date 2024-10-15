using DG.Tweening;
using UnityEngine;

namespace _Project.SimpleTweenAnimations.Scripts.Views
{
    public class PunchScaleUIAnimationView : SimpleTweenAnimation
    {
        [field: SerializeField] private RectTransform AnimationNode { get; set; }
        [field: SerializeField] private Vector3 Punch { get; set; } = new Vector3(0.1f, 0.1f, 0f);
        [field: SerializeField] private int Vibrato { get; set; } = 10;
        [field: SerializeField] private float Elasticity { get; set; }
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

            AnimationTween = AnimationNode.DOPunchScale(Punch, AnimationDuration, Vibrato, Elasticity)
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
