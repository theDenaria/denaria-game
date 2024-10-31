using _Project.StrangeIOCUtility.Scripts.Views;
using UnityEngine;

namespace _Project.Player.Scripts.Views
{
    public class BasePlayerAnimationView : ViewZeitnot
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