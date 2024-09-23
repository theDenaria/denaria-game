using _Project.ApplicationMemoryTracker.Scripts.Enums;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using DG.Tweening;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.ApplicationMemoryTracker.Scripts.Views
{
    [RequireComponent(typeof(Canvas), typeof(CanvasGroup), typeof(GraphicRaycaster))]
    public class ApplicationMemoryCriticalCanvasView : View
    {
        [field: SerializeField] private Canvas ViewCanvas { get; set; }
        [field: SerializeField] private CanvasGroup ViewCanvasGroup { get; set; }
        //[field: SerializeField] private ButtonZeitnot ClearMemoryButton { get; set; }
        [field: SerializeField] private ButtonZeitnot QuitApplicationButton { get; set; }
        [field: SerializeField] private DOTweenAnimation MemoryCleaningInProgressAnimation { get; set; }
        [field: SerializeField] private string MemoryCleaningInProgressAnimationID { get; set; } = "MemoryCleaningInProgress";

        internal Signal OnViewShownSignal { get; } = new Signal();
        internal Signal OnClearMemoryButtonClickedSignal { get; } = new Signal();
        internal Signal OnQuitApplicationButtonClickedSignal { get; } = new Signal();

        public bool IsShown { get; private set; } = false;
        private PopupWindowAppearanceStates AppearanceState { get; set; } = PopupWindowAppearanceStates.Unknown;
        private bool IsIOSPlatform { get; } =
#if UNITY_IOS
            true;
#else
            false;
#endif

        protected override void Awake()
        {
            base.Awake();
            QuitApplicationButton.gameObject.SetActive(!IsIOSPlatform);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            //ClearMemoryButton.onClick.AddListener(OnClearMemoryButtonClickedEvent);
            QuitApplicationButton.onClick.AddListener(OnQuitApplicationButtonClickedEvent);
        }

        protected override void OnDisable()
        {
            //ClearMemoryButton.onClick.RemoveListener(OnClearMemoryButtonClickedEvent);
            QuitApplicationButton.onClick.RemoveListener(OnQuitApplicationButtonClickedEvent);
            StopMemoryCleaningInProgressAnimation();
            base.OnDisable();
        }

        protected override void OnDestroy()
        {
            if (MemoryCleaningInProgressAnimation != null && MemoryCleaningInProgressAnimation.isActive)
            {
                MemoryCleaningInProgressAnimation.DOKillAllById(MemoryCleaningInProgressAnimationID);
            }
            base.OnDestroy();
        }

        private void OnClearMemoryButtonClickedEvent()
        {
            OnClearMemoryButtonClickedSignal.Dispatch();
        }

        private void OnQuitApplicationButtonClickedEvent()
        {
            OnQuitApplicationButtonClickedSignal.Dispatch();
        }

        public void Show()
        {
            if (AppearanceState == PopupWindowAppearanceStates.Showing || AppearanceState == PopupWindowAppearanceStates.Shown)
            {
                return;
            }
            AppearanceState = PopupWindowAppearanceStates.Showing;

            // TODO: We can implement an animation here, but when the animation ends, we must call OnShown method to make sure about the Canvas setup is valid after the animation.
            PlayMemoryCleaningInProgressAnimation();
            OnShown();
        }

        public void Close()
        {
            if (AppearanceState == PopupWindowAppearanceStates.Closing || AppearanceState == PopupWindowAppearanceStates.Closed)
            {
                return;
            }
            AppearanceState = PopupWindowAppearanceStates.Closing;
            IsShown = false;
            // TODO: We can implement an animation here, but when the animation ends, we must call OnShown method to make sure about the Canvas setup is valid after the animation.
            OnClosed();
        }

        private void OnShown()
        {
            ViewCanvasGroup.blocksRaycasts = true;
            ViewCanvasGroup.interactable = true;
            ViewCanvasGroup.alpha = 1f;
            ViewCanvas.enabled = true;
            IsShown = true;
            AppearanceState = PopupWindowAppearanceStates.Shown;
            OnViewShownSignal.Dispatch();
        }

        private void OnClosed()
        {
            StopMemoryCleaningInProgressAnimation();
            ViewCanvasGroup.blocksRaycasts = false;
            ViewCanvasGroup.alpha = 0f;
            ViewCanvasGroup.interactable = false;
            ViewCanvas.enabled = false;
            AppearanceState = PopupWindowAppearanceStates.Closed;
        }

        private void PlayMemoryCleaningInProgressAnimation()
        {
            StopMemoryCleaningInProgressAnimation();
            MemoryCleaningInProgressAnimation.DOPlayAllById(MemoryCleaningInProgressAnimationID);
        }

        private void StopMemoryCleaningInProgressAnimation()
        {
            MemoryCleaningInProgressAnimation.DORewindAllById(MemoryCleaningInProgressAnimationID);
        }
    }
}