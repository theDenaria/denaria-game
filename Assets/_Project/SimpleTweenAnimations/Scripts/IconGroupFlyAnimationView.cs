using _Project.StrangeIOCUtility;
using _Project.UIZeitnot.ImageZeitnot;
using DG.Tweening;
using UnityEngine;

namespace _Project.SimpleTweenAnimations.Scripts
{
    public class IconGroupFlyAnimationView : ViewZeitnot
    {
        [field: SerializeField] private float AnimationStartDelay { get; set; } = 0f;

        [field: SerializeField] private RectTransform ToRectTransform { get; set; }
        [field: SerializeField] private ImageZeitnot[] Icons { get; set; }
        [field: SerializeField] private float DelayBetweenIcons { get; set; } = 0.075f;
        [field: SerializeField] private float AnimationDuration { get; set; } = 0.4f;
        [field: SerializeField] private Ease MoveEase { get; set; } = Ease.InOutSine;

        [field: SerializeField] private bool DoIconScale { get; set; } = false;
        [field: SerializeField] private float StartIconScale { get; set; } = 1f;
        [field: SerializeField] private float EndIconScale { get; set; } = 0.1f;
        [field: SerializeField] private Ease IconScaleEase { get; set; } = Ease.Linear;

        [field: SerializeField] private bool DoIconFade { get; set; } = false;
        [field: SerializeField] private float StartIconFade { get; set; } = 1f;
        [field: SerializeField] private float EndIconFade { get; set; } = 0.1f;
        [field: SerializeField] private Ease IconFadeEase { get; set; } = Ease.Linear;
        
        private Sequence AnimationSequence { get; set; } = null;
        private RectTransform[] IconRectTransforms { get; set; }
        
        protected override void Awake()
        {
            base.Awake();
            CacheIconRectTransforms();
        }

        private void OnDisable()
        {
            KillAnimation();
        }

        protected override void OnDestroy()
        {
            IconRectTransforms = null;
            base.OnDestroy();
        }


        [Sirenix.OdinInspector.Button("Play")]
        public void Play(RectTransform from)
        {
            KillAnimation();
            CreateAnimation(from, ToRectTransform);
        }

        public void Play(RectTransform from, RectTransform to)
        {
            KillAnimation();
            CreateAnimation(from, to);
        }
        
        private void CreateAnimation(RectTransform from, RectTransform to)
        {
            AnimationSequence = DOTween.Sequence()
                .SetLoops(1)
                .SetId(this)
                .SetAutoKill(true)
                .OnKill(() => { AnimationSequence = null; });

            int iconCount = Icons.Length;
            if (iconCount < 1) { return; }

            for (int i = 0; i < iconCount; ++i)
            {
                float insertAtPosition = AnimationStartDelay + i * DelayBetweenIcons;
                RectTransform iconRectTransform = IconRectTransforms[i];
                ImageZeitnot iconImage = Icons[i];
                Tween moveAnimationTween = CreateMoveAnimationTween(iconRectTransform, iconImage, from, to);
                AnimationSequence.Insert(insertAtPosition, moveAnimationTween);

                if (DoIconScale)
                {
                    Tween scaleAnimationTween = CreateScaleAnimationTween(iconRectTransform);
                    AnimationSequence.Insert(insertAtPosition, scaleAnimationTween);
                }

                if (DoIconFade)
                {
                    Tween fadeAnimationTween = CreateFadeAnimationTween(iconImage);
                    AnimationSequence.Insert(insertAtPosition, fadeAnimationTween);
                }
            }
        }

        private Tween CreateMoveAnimationTween(RectTransform iconRectTransform, ImageZeitnot iconImage, RectTransform moveFrom, RectTransform moveTo)
        {
            iconRectTransform.position = moveFrom.position;
            return iconRectTransform.DOMove(moveTo.position, AnimationDuration, false)
                .SetLoops(1)
                .SetId(this)
                .SetEase(MoveEase)
                .OnStart(() =>
                {
                    iconImage.enabled = true;
                })
                .OnComplete(() =>
                {
                    iconImage.enabled = false;
                });
        }

        private Tween CreateScaleAnimationTween(RectTransform iconRectTransform)
        {
            iconRectTransform.localScale = Vector3.one * StartIconScale;
            return iconRectTransform.DOScale(EndIconScale, AnimationDuration)
                .SetLoops(1)
                .SetId(this)
                .SetEase(IconScaleEase);
        }

        private Tween CreateFadeAnimationTween(ImageZeitnot iconImage)
        {
            Color iconImageColor = iconImage.color;
            iconImageColor.a = StartIconFade;
            iconImage.color = iconImageColor;
            return iconImage.DOFade(EndIconFade, AnimationDuration)
                .SetLoops(1)
                .SetId(this)
                .SetEase(IconFadeEase);
        }

        private void KillAnimation()
        {
            if (AnimationSequence == null) { return; }
            if (AnimationSequence.IsActive())
            {
                AnimationSequence.Kill(false);
            }
            AnimationSequence = null;
        }

        private void CacheIconRectTransforms()
        {
            int iconCount = Icons.Length;
            IconRectTransforms = new RectTransform[iconCount];
            for (int i = 0; i < iconCount; ++i)
            {
                IconRectTransforms[i] = Icons[i].GetComponent<RectTransform>();
            }
        }

    }
}
