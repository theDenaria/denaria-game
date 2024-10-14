using _Project.GameSceneManager.Scripts.Signals;
using _Project.Login.Scripts.Signals;
using strange.extensions.mediation.impl;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Animation.Scripts.Views
{
    public class PlayerAnimationMediator : Mediator
    {
        [Inject] public PlayerAnimationView PlayerAnimationView { get; set; }

        private bool _isMoving;
        private bool _isGrounded;


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
            if (!_isGrounded)
            {
                if (input != Vector2.zero)
                {
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
                else if (_isMoving && input == Vector2.zero)
                {
                    _isMoving = false;
                    PlayerAnimationView.SetMoveState(0);
                    PlayerAnimationView.playerAnimator.SetBool("isMoving", false);
                }
            }
        }
        [ListensTo(typeof(PlayerJumpInputSignal))]
        public void SetJump()
        {
            PlayerAnimationView.TriggerJumpStart();
        }

        [ListensTo(typeof(OnLandColliderEnterSignal))]
        public void OnLandColliderEnter()
        {
            PlayerAnimationView.TriggerLandColliderEnter();
            _isGrounded = true;
        }

        [ListensTo(typeof(OnLandColliderExitSignal))]
        public void OnLandColliderExit()
        {
            _isGrounded = false;
            _isMoving = false;
            PlayerAnimationView.TriggerLandColliderExit();
        }
    }
}
