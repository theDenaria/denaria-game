using System.Collections;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using _Project.Utilities;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.DeviceStorageTracker.Scripts.Views
{
    [RequireComponent(typeof(Canvas), typeof(CanvasGroup), typeof(GraphicRaycaster))]
    public class InsufficientStorageSpaceCanvasView : View
    {
        [field: SerializeField] private Canvas ViewCanvas { get; set; }
        [field: SerializeField] private CanvasGroup ViewCanvasGroup { get; set; }
        [field: SerializeField] private ButtonZeitnot CleanStorageButton { get; set; }
        [field: SerializeField] private ButtonZeitnot QuitApplicationButton { get; set; }

        internal Signal onCleanStorageButtonClickedSignal { get; } = new Signal();
        internal Signal onQuitApplicationButtonClickedSignal { get; } = new Signal();

        public bool IsShown { get; private set; } = false;
        private Coroutine CleanStorageButtonSleepRoutine { get; set; } = null;
        private WaitForSecondsRealtime WaitForCleanStorageButtonSleepTime { get; } = new WaitForSecondsRealtime(Constants.CLEAN_STORAGE_UP_BUTTON_SLEEP_TIME);

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
            CleanStorageButton.onClick.AddListener(OnCleanStorageButtonClickedEvent);
            QuitApplicationButton.onClick.AddListener(OnQuitApplicationButtonClickedEvent);
        }

        protected override void OnDisable()
        {
            StopCleanStorageButtonSleepRoutine();
            CleanStorageButton.onClick.RemoveListener(OnCleanStorageButtonClickedEvent);
            QuitApplicationButton.onClick.RemoveListener(OnQuitApplicationButtonClickedEvent);
            base.OnDisable();
        }

        private void OnCleanStorageButtonClickedEvent()
        {
            CleanStorageButton.interactable = false;
            StartCleanStorageButtonSleepRoutine();
            onCleanStorageButtonClickedSignal.Dispatch();
        }

        private void OnQuitApplicationButtonClickedEvent()
        {
            onQuitApplicationButtonClickedSignal.Dispatch();
        }

        public void Show()
        {
            // TODO: We can implement an animation here, but when the animation ends, we must call OnShown method to make sure about the Canvas setup is valid after the animation.
            OnShown();
        }

        public void Close()
        {
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
        }

        private void OnClosed()
        {
            ViewCanvasGroup.blocksRaycasts = false;
            ViewCanvasGroup.alpha = 0f;
            ViewCanvasGroup.interactable = false;
            ViewCanvas.enabled = false;
        }

        private void StartCleanStorageButtonSleepRoutine()
        {
            StopCleanStorageButtonSleepRoutine();
            CleanStorageButtonSleepRoutine = StartCoroutine(CoCleanStorageButtonSleepRoutine());
        }

        private void StopCleanStorageButtonSleepRoutine()
        {
            if (CleanStorageButtonSleepRoutine != null)
            {
                StopCoroutine(CleanStorageButtonSleepRoutine);
                CleanStorageButtonSleepRoutine = null;
            }
        }

        private IEnumerator CoCleanStorageButtonSleepRoutine()
        {
            yield return WaitForCleanStorageButtonSleepTime;
            CleanStorageButton.interactable = true;
            CleanStorageButtonSleepRoutine = null;
        }

    }
}