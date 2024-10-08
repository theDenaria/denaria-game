using System;
using DG.Tweening;
using UnityEngine;

namespace _Project.SimpleTweenAnimations.Scripts.Views
{
    public class RotateAnimationView : SimpleTweenAnimation
    {
        [field: SerializeField] private Transform RotationPivot { get; set; }
        [field: SerializeField] private RotationSpaceTypes RotationSpaceType { get; set; } = RotationSpaceTypes.Local;
        [field: SerializeField] private Vector3 RotationStartEulerAngles { get; set; } = Vector3.zero;
        [field: SerializeField] private Vector3 RotationEndEulerAngles { get; set; } = new Vector3(0f, 0f, -360f);
        [field: SerializeField] private float AnimationDuration { get; set; } = 1f;
        [field: SerializeField] private RotateMode RotateMode { get; set; } = RotateMode.FastBeyond360;
        [field: SerializeField] private int LoopCount { get; set; } = -1;
        [field: SerializeField] private Ease Ease { get; set; } = Ease.Linear;
        
        private Tween AnimationTween { get; set; } = null;

        private void OnEnable()
        {
            if (PlayOnEnable)
            {
                Play();
            }
        }

        private void OnDisable()
        {
            KillAnimation();
        }

        public override void Play(bool killExisting = false)
        {
            CreateAnimation(killExisting);
        }

        public override void Stop()
        {
            KillAnimation();
        }

        protected override void CreateAnimation(bool killExisting = false)
        {
            if (killExisting)
            {
                KillAnimation();
            }
            else if (AnimationTween != null)
            {
                return;
            }

            CreateTweenAnimationBase()
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

        private Tween CreateTweenAnimationBase()
        {
            return (RotationSpaceType == RotationSpaceTypes.Local) ?
                CreateLocalSpaceRotationTweenAnimationBase() :
                CreateWorldSpaceRotationTweenAnimationBase();
        }

        private Tween CreateLocalSpaceRotationTweenAnimationBase()
        {
            RotationPivot.localRotation = Quaternion.Euler(RotationStartEulerAngles);
            return AnimationTween = RotationPivot.DOLocalRotate(RotationEndEulerAngles, AnimationDuration, RotateMode);
        }

        private Tween CreateWorldSpaceRotationTweenAnimationBase()
        {
            RotationPivot.rotation = Quaternion.Euler(RotationStartEulerAngles);
            return AnimationTween = RotationPivot.DORotate(RotationEndEulerAngles, AnimationDuration, RotateMode);
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


        #region Nested Enums

        [Serializable]
        private enum RotationSpaceTypes
        {
            /// <summary>
            ///     Rotates at its own local axis
            /// </summary>
            Local = 0,

            /// <summary>
            ///     Rotates at global/world axis
            /// </summary>
            World = 1
        }

        #endregion

    }
}
