using _Project.InputManager.Scripts.Signals;
using _Project.Login.Scripts.Signals;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace _Project.Animation.Scripts.Views
{
    public class PlayerAnimationMediator : Mediator
    {
        [Inject] public PlayerAnimationView PlayerAnimationView { get; set; }

        private bool _isMoving;
        private bool _isGrounded;
        private int _groundCollidersCount;

        public override void OnRegister()
        {
            // PlayerMovementAnimationSignal.AddListener(SetAnimationState);
            // PlayerMovementAnimationSignal.AddListener(StopAnimationState);
        }

        public override void OnRemove()
        {
            // PlayerMovementAnimationSignal.RemoveListener(SetAnimationState);
            // PlayerMovementAnimationSignal.RemoveListener(StopAnimationState);
        }

        [ListensTo(typeof(PlayerMoveInputSignal))]
        public void SetMoving(Vector2 input)
        {
            if (input == Vector2.zero)
            {
                _isMoving = false;
                PlayerAnimationView.SetMoveState(0);
                PlayerAnimationView.playerAnimator.SetBool("isMoving", false);
            }
            else
            {
                if (!_isGrounded)
                {
                    return;
                }
                _isMoving = true;
                if (input.y > 0)
                {
                    PlayerAnimationView.SetMoveState(1);
                }
                else if (input.y < 0)
                {
                    PlayerAnimationView.SetMoveState(-1);
                }
                else
                {
                    if (input.x > 0)
                    {
                        PlayerAnimationView.SetMoveState(2);
                    }
                    else if (input.x < 0)
                    {
                        PlayerAnimationView.SetMoveState(3);
                    }
                }
                PlayerAnimationView.playerAnimator.SetBool("isMoving", true);

            }
        }
        [ListensTo(typeof(PlayerJumpInputSignal))]
        public void SetJump()
        {
            if (!_isGrounded)
            {
                return;
            }
            PlayerAnimationView.TriggerJumpStart();
        }

        [ListensTo(typeof(OnLandColliderEnterSignal))]
        public void OnLandColliderEnter()
        {
            _groundCollidersCount++;
            if (_groundCollidersCount > 0 && !_isGrounded)
            {
                PlayerAnimationView.TriggerLandColliderEnter();
                _isGrounded = true;
            }
        }

        [ListensTo(typeof(OnLandColliderExitSignal))]
        public void OnLandColliderExit()
        {
            _groundCollidersCount--;
            if (_groundCollidersCount <= 0)
            {
                PlayerAnimationView.TriggerLandColliderExit();
                _isGrounded = false;
                _isMoving = false;
            }
        }

    }
}
