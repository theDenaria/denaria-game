using System;
using _Project.StrangeIOCUtility;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Project.Animation.Scripts.Views
{
    public class PlayerAnimationView : ViewZeitnot
    {
        public Animator playerAnimator;

        private void OnEnable()
        {

        }
        private void OnDisable()
        {

        }

        public void SetMoveState(int stateValue)
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetInteger("moveState", stateValue);
            }
            else
            {
                Debug.LogError("Animator not assigned to PlayerAnimationView.");
            }
        }

        public void TriggerJumpStart()
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("jumpStart");
            }
        }

        public void TriggerLandColliderEnter()
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("landColliderEnter");
            }
        }

        public void TriggerLandColliderExit()
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("landColliderExit");
            }
        }
    }
}
