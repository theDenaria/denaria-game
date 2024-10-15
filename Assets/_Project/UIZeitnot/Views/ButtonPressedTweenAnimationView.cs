using _Project.StrangeIOCUtility;
using _Project.StrangeIOCUtility.Scripts.Views;
using ButtonZeit = _Project.UIZeitnot.ButtonZeitnot.Scripts.ButtonZeitnot;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.UIZeitnot.Views
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(ButtonZeit))]
    public class ButtonPressedTweenAnimationView : ViewZeitnot, IPointerDownHandler, IPointerUpHandler
    {
        [field: SerializeField] private float NormalScale { get; set; } = 1f;
        [field: SerializeField] private float ToNormalDuration { get; set; } = 0.2f;
        [field: SerializeField] private Ease ToNormalEase { get; set; } = Ease.InOutSine;

        [field: SerializeField] private float PressedScale { get; set; } = 0.95f;
        [field: SerializeField] private float ToPressedDuration { get; set; } = 0.2f;
        [field: SerializeField] private Ease ToPressedEase { get; set; } = Ease.InOutSine;

        private RectTransform RectTransform { get; set; }
        private ButtonZeit Button { get; set; }
        private float CurrentScale { get; set; } = 1f;
        private Tween ActiveTransitionAnimation { get; set; }

        protected override void Awake()
        {
            base.Awake();
            RectTransform = GetComponent<RectTransform>();
            Button = GetComponent<ButtonZeit>();
        }

        protected override void OnDestroy()
        {
            KillTransitionTweenAnimations();
            RectTransform = null;
            base.OnDestroy();
        }

        private void KillTransitionTweenAnimations()
        {
            KillActiveTransitionAnimation();
        }

        private void CreateAndPlayTransitionAnimation(float targetScale, float duration, Ease ease)
        {
            KillActiveTransitionAnimation();

            // Create tween animation
            ActiveTransitionAnimation = DOTween.To(
                () => CurrentScale,
                x => CurrentScale = x,
                targetScale,
                duration
            );

            // Configure tween animation
            ActiveTransitionAnimation.SetEase(ease)
                .SetLoops(1)
                .SetId(this)
                .SetAutoKill(true)
                .OnKill(() =>
                {
                    ActiveTransitionAnimation = null;
                })
                .OnUpdate(() =>
                {
                    RectTransform.localScale = Vector3.one * CurrentScale;
                }
            );
        }

        private void PlayToNormalTransitionAnimation()
        {
            KillActiveTransitionAnimation();
            CreateAndPlayTransitionAnimation(NormalScale, ToNormalDuration, ToNormalEase);
        }

        private void PlayToPressedTransitionAnimation()
        {
            KillActiveTransitionAnimation();
            CreateAndPlayTransitionAnimation(PressedScale, ToPressedDuration, ToPressedEase);
        }

        private void KillActiveTransitionAnimation()
        {
            if (ActiveTransitionAnimation != null)
            {
                if (ActiveTransitionAnimation.IsActive())
                {
                    ActiveTransitionAnimation.Kill(false);
                }
                ActiveTransitionAnimation = null;
            }
        }


        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (Button.interactable)
            {
                PlayToPressedTransitionAnimation();
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            PlayToNormalTransitionAnimation();
        }

    }
}
