using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;

    private ClientBehaviour clientBehaviour;

    private PlayerInputHandler playerInputHandler;

    private Camera mainCamera;

    public string playerId;

    private Quaternion lastSentRotation;

    // Define the tick rate (e.g., 20 ticks per second)
    public float tickRate = 30.0f;

    // Variable to keep track of the elapsed time
    private float elapsedTime = 0.0f;

    // Interval between ticks in seconds
    private float tickInterval;

    [SerializeField] Transform shoulderTransform;

    private PlayerAim playerAimScript;

    public float health;

    [SerializeField] private Transform barrelPosition;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        clientBehaviour = ClientBehaviour.Instance;
        playerInputHandler = PlayerInputHandler.Instance;
        mainCamera = Camera.main;
        playerAimScript = GetComponent<PlayerAim>();
        playerAimScript.enabled = false;
    }

    private void Start()
    {
        health = 100f;
        lastSentRotation = transform.rotation;
        tickInterval = 1.0f / tickRate;
    }

    private void Update()
    {
        if (gameManager.ownPlayerId == playerId)
        {
            elapsedTime += Time.deltaTime;
            if (!gameManager.isMenuOpen)
            {
                HandleRotation();
                if (elapsedTime >= tickInterval)
                {
                    SendMoveInputToServer();
                    SendRotationToServer();
                    SendJumpInputToServer();
                    SendFireActionToServer();

                    // Reset the elapsed time, accounting for potential accumulated time
                    elapsedTime -= tickInterval;
                }
            }
        }
    }

    void SendMoveInputToServer()
    {
        if (playerInputHandler == null)
        {
            Debug.LogError("PlayerInputHandler not found in the scene!");
            return;
        }
        if (playerInputHandler.MoveInput != Vector2.zero)
        {
            Vector2 inputDirection = new(playerInputHandler.MoveInput.x, playerInputHandler.MoveInput.y);
            var rotatedMove = GetRotatedInput(inputDirection.normalized);
            clientBehaviour.SendMovement(rotatedMove);
        }
    }

    void SendJumpInputToServer()
    {
        if (playerInputHandler == null)
        {
            Debug.LogError("PlayerInputHandler not found in the scene!");
            return;
        }
        if (playerInputHandler.JumpTriggered == true)
        {
            clientBehaviour.SendJump();
        }
    }

    void SendFireActionToServer()
    {
        if (playerInputHandler == null)
        {
            Debug.LogError("PlayerInputHandler not found in the scene!");
            return;
        }
        if (playerInputHandler.FireTriggered == true)
        {
            Vector3 screenPoint = new(Screen.width / 2, Screen.height / 2, 0);

            // Create a ray from the camera to the screen point
            Ray ray = mainCamera.ScreenPointToRay(screenPoint);

            clientBehaviour.SendFire(ray.origin, ray.direction, barrelPosition.position);
        }
    }

    void HandleRotation()
    {
        float yRotation = mainCamera.transform.eulerAngles.y;
        float xRotation = mainCamera.transform.eulerAngles.x;
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        shoulderTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void SendRotationToServer()
    {
        Quaternion currentRotation = transform.rotation;
        float angleDifference = Quaternion.Angle(lastSentRotation, currentRotation);
        if (angleDifference > 5.0)
        {
            // Combine into a single Vector4
            Vector4 quaternionData = new(currentRotation.x, currentRotation.y, currentRotation.z, currentRotation.w);
            clientBehaviour.SendRotation(quaternionData);
            lastSentRotation = currentRotation;
        }
    }

    public void SetPlayerId(string id)
    {
        playerId = id;
        if (gameManager.ownPlayerId == playerId)
        {
            playerAimScript.enabled = true;
        }
    }


    Vector2 GetRotatedInput(Vector2 normalizedInput)
    {
        float angleDegrees = transform.eulerAngles.y;
        float angleRadians = angleDegrees * Mathf.Deg2Rad; // Convert degrees to radians
        float cosAngle = Mathf.Cos(angleRadians);
        float sinAngle = Mathf.Sin(angleRadians);

        float rotatedX = normalizedInput.x * cosAngle + normalizedInput.y * sinAngle;
        float rotatedY = -normalizedInput.x * sinAngle + normalizedInput.y * cosAngle;

        return new Vector2(rotatedX, rotatedY);
    }
}
