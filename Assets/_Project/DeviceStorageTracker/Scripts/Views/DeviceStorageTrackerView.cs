using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using _Project.Utilities;

namespace _Project.DeviceStorageTracker.Scripts.Views
{
    public class DeviceStorageTrackerView : View
    {
        internal Signal onCheckStorageStatusSignal = new Signal();

        private WaitForSecondsRealtime WaitForNextStorageStatusCheck { get; set; } = new WaitForSecondsRealtime(Constants.DEVICE_STORAGE_CHECK_INTERVAL_SEC);
        private Coroutine StorageCheckerRoutine { get; set; } = null;

        protected override void OnEnable()
        {
            base.OnEnable();
            StartStorageCheckerRoutine();
        }

        protected override void OnDisable()
        {
            StopStorageCheckerRoutine();
            base.OnDisable();
        }

        private void StartStorageCheckerRoutine()
        {
            StorageCheckerRoutine = StartCoroutine(CoStorageCheckerRoutine());
        }

        private void StopStorageCheckerRoutine()
        {
            if (StorageCheckerRoutine != null)
            {
                StopCoroutine(StorageCheckerRoutine);
                StorageCheckerRoutine = null;
            }
        }

        private IEnumerator CoStorageCheckerRoutine()
        {
            yield return new WaitForEndOfFrame();
            while (enabled)
            {
                onCheckStorageStatusSignal.Dispatch();
                yield return WaitForNextStorageStatusCheck;
            }
            StorageCheckerRoutine = null;
        }
    }
}