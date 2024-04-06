using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float sprintMultiplier = 2f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 9.81f;

    // [Header("Look Sensitivity")]
    // [SerializeField] private float mouseSensitivity = 2f;
    // [SerializeField] private float upDownRange = 80f;

    private CharacterController characterController;
    private Camera mainCamera;
    private PlayerInputHandler inputHandler;
    private Vector3 currentMovement;
    // private float verticalRotation;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        inputHandler = PlayerInputHandler.Instance;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float speed = walkSpeed * (inputHandler.SprintValue > 0 ? sprintMultiplier : 1f);

        Vector3 inputDirection = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        worldDirection.Normalize();

        currentMovement.x = worldDirection.x * speed;
        currentMovement.z = worldDirection.z * speed;

        HandleJumping();

        characterController.Move(currentMovement * Time.deltaTime);
    }

    void HandleJumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;
            if (inputHandler.JumpTriggered)
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }

    void HandleRotation()
    {
        // Capture the Y-axis rotation of the camera.
        float yRotation = mainCamera.transform.eulerAngles.y;

        // Apply the Y-axis rotation to the player.
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
