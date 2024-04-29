using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    // [Header("Movement Speeds")]
    // [SerializeField] private float walkSpeed = 6f;
    // [SerializeField] private float sprintMultiplier = 2f;

    // [Header("Jump Parameters")]
    // [SerializeField] private float jumpForce = 5f;
    // [SerializeField] private float gravity = 9.81f;

    [SerializeField] private ClientBehaviour clientBehaviour;

    [SerializeField] private Transform playerTransform;

    // [Header("Look Sensitivity")]
    // [SerializeField] private float mouseSensitivity = 2f;
    // [SerializeField] private float upDownRange = 80f;

    // private CharacterController characterController;
    private Camera mainCamera;
    private PlayerInputHandler inputHandler;
    private Vector3 currentMovement;
    // private float verticalRotation;
    private void Start()
    {
        // characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        inputHandler = PlayerInputHandler.Instance;
    }

    private void Update()
    {
        // HandleMovement();
        HandleRotation();
        SendInputToServer();
    }

    void SendInputToServer()
    {
        if (inputHandler.MoveInput != Vector2.zero)
        {
            Vector2 inputDirection = new Vector2(inputHandler.MoveInput.x, inputHandler.MoveInput.y);
            clientBehaviour.SendMovement(inputDirection.normalized);
        }
    }

    public void HandleMovement(Vector2 position)
    {
        playerTransform.position = new Vector3(position.x, playerTransform.position.y, position.y);
    }


    IEnumerator UpdatePosition(Vector3 start, Vector3 end, float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            playerTransform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        playerTransform.position = end; // Ensure the final position is set precisely
    }

    // void HandleJumping()
    // {
    //     if (characterController.isGrounded)
    //     {
    //         currentMovement.y = -0.5f;
    //         if (inputHandler.JumpTriggered)
    //         {
    //             currentMovement.y = jumpForce;
    //         }
    //     }
    //     else
    //     {
    //         currentMovement.y -= gravity * Time.deltaTime;
    //     }
    // }

    void HandleRotation()
    {
        // Capture the Y-axis rotation of the camera.
        float yRotation = mainCamera.transform.eulerAngles.y;

        // Apply the Y-axis rotation to the player.
        playerTransform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
