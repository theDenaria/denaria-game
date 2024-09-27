using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using _Project.Utilities;
using UnityEngine;

namespace _Project.ApplicationMemoryTracker.Scripts.Views
{
    public class ApplicationMemoryTrackerView : View
    {
        internal Signal OnNextApplicationMemoryCheckSignal { get; private set; } = new Signal();

        internal bool IsMemoryTrackingActive
        {
            get { return MemoryTrackerRoutine != null; }
        }

        private WaitForSeconds WaitForNextMemoryCheck { get; set; } = new WaitForSeconds(Constants.APPLICATION_MEMORY_CHECK_INTERVAL_SEC);
        private Coroutine MemoryTrackerRoutine { get; set; } = null;

        protected override void OnEnable()
        {
            base.OnEnable();
            StartMemoryTracking();
            //StartDummyTextureSpawner();
        }

        protected override void OnDisable()
        {
            //StopDummyTextureSpawner();
            StopMemoryTracking();
            base.OnDisable();
        }

        private void StartMemoryTracking()
        {
            StopMemoryTracking();
            MemoryTrackerRoutine = StartCoroutine(CoMemoryTrackerRoutine());
        }

        private void StopMemoryTracking()
        {
            if (MemoryTrackerRoutine != null)
            {
                StopCoroutine(MemoryTrackerRoutine);
                MemoryTrackerRoutine = null;
            }
        }

        private IEnumerator CoMemoryTrackerRoutine()
        {
            yield return new WaitForEndOfFrame();   // We wait 1 Unity frame to be sure about StrangeIoC bindings are completed.
            while (isActiveAndEnabled)
            {
                OnNextApplicationMemoryCheckSignal.Dispatch();
                yield return WaitForNextMemoryCheck;
            }
            MemoryTrackerRoutine = null;
        }


        // TODO: Delete the "dummy texture spawner" block after application memory tests complete.
        // Dummy texture spawner block (to infilate memory for testing stuff)
        /*
        private Coroutine DummyTextureSpawnerRoutine { get; set; } = null;
        private void StartDummyTextureSpawner()
        {
            StopDummyTextureSpawner();
            DummyTextureSpawnerRoutine = StartCoroutine(CoDummyTextureSpawner());
        }
        private void StopDummyTextureSpawner()
        {
            if (DummyTextureSpawnerRoutine != null)
            {
                StopCoroutine(DummyTextureSpawnerRoutine);
                DummyTextureSpawnerRoutine = null;
            }
        }
        private IEnumerator CoDummyTextureSpawner()
        {
            yield return new WaitForSeconds(5f);
            YieldInstruction waitFor = new WaitForSeconds(0.5f);
            while (enabled)
            {
                yield return waitFor;
                Texture2D newTexture = new Texture2D(1024, 1024);
                newTexture = null;
            }
        }
        */

    }
}