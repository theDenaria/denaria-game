using _Project.UIZeitnot.PanelZeitnot.Scripts.Enums;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.UIZeitnot.PanelZeitnot.Scripts
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(GraphicRaycaster))]
    [RequireComponent(typeof(DOTweenAnimation))]
    public class PanelZeitnot : MonoBehaviour
    {
        public event Action<PanelZeitnotVisibilityTypes> OnPanelVisibilityChangedEvent = delegate { };
        
        [field: SerializeField] private Canvas Canvas { get; set; }
        [field: SerializeField] private CanvasGroup CanvasGroup { get; set; }
        [field: SerializeField] private string FadeInAnimationID { get; set; } = "FadeIn";
        [field: SerializeField] private string FadeOutAnimationID { get; set; } = "FadeOut";

        public PanelZeitnotVisibilityTypes VisibilityType { get; private set; }

        private DOTweenAnimation DoTweenAnimationInstance { get; set; } = null;

        protected virtual void Awake()
        {
            DoTweenAnimationInstance = GetComponent<DOTweenAnimation>();
            CanvasGroup.alpha = 0f;
            CanvasGroup.blocksRaycasts = false;
            CanvasGroup.interactable = false;
            Canvas.enabled = false;
            VisibilityType = PanelZeitnotVisibilityTypes.Hidden;
        }

        protected virtual void OnDestroy()
        {
            OnPanelVisibilityChangedEvent = null;
            DoTweenAnimationInstance = null;
        }

        public void OnFadingInStart()
        {
            VisibilityType = PanelZeitnotVisibilityTypes.Showing;
            CanvasGroup.alpha = 0f;
            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.interactable = false;
            Canvas.enabled = true;
            OnPanelVisibilityChangedEvent(VisibilityType);
        }

        public void OnFadingInComplete()
        {
            CanvasGroup.alpha = 1f;
            CanvasGroup.interactable = true;
            VisibilityType = PanelZeitnotVisibilityTypes.Shown;
            OnPanelVisibilityChangedEvent(VisibilityType);
        }

        public void OnFadingOutStart()
        {
            VisibilityType = PanelZeitnotVisibilityTypes.Hiding;
            Canvas.enabled = true;
            CanvasGroup.alpha = 1f;
            CanvasGroup.blocksRaycasts = true;
            CanvasGroup.interactable = false;
            OnPanelVisibilityChangedEvent(VisibilityType);
        }

        public void OnFadingOutComplete()
        {
            CanvasGroup.alpha = 0f;
            CanvasGroup.blocksRaycasts = false;
            Canvas.enabled = false;
            VisibilityType = PanelZeitnotVisibilityTypes.Hidden;
            OnPanelVisibilityChangedEvent(VisibilityType);
        }

        [BoxGroup("ControlGroup")]
        [HorizontalGroup("ControlGroup/ControlButtons")]
        [Button("Show")]
        internal void Show()
        {
            PlayFadingAnimation(FadeInAnimationID);
        }

        [HorizontalGroup("ControlGroup/ControlButtons")]
        [Button("Hide")]
        internal void Hide()
        {
            PlayFadingAnimation(FadeOutAnimationID);
        }

        private void PlayFadingAnimation(string animationID)
        {
            DoTweenAnimationInstance.DORewind();
            DoTweenAnimationInstance.DORestartAllById(animationID);
        }

    }
}
