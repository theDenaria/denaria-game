using _Project.StrangeIOCUtility;
using strange.extensions.signal.impl;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Animation.Scripts.Views
{
    public class GroundColliderView : ViewZeitnot
    {
        internal Signal onLandColliderEnter = new Signal();
        internal Signal onLandColliderExit = new Signal();

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                onLandColliderEnter.Dispatch();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                onLandColliderExit.Dispatch();
            }
        }
    }
}