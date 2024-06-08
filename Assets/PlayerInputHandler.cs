using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map References")]
    [SerializeField] private string playerActionMapName = "Player";
    [SerializeField] private string uiActionMapName = "UI";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string fire = "Fire";
    [SerializeField] private string sprint = "Sprint";

    [SerializeField] private string escMenu = "EscMenu";


    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction fireAction;
    private InputAction sprintAction;

    public InputAction escMenuAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTriggered { get; private set; }

    public bool FireTriggered { get; private set; }
    public float SprintValue { get; private set; }

    public bool EscMenuTriggered { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap(playerActionMapName).FindAction(move);
        lookAction = playerControls.FindActionMap(playerActionMapName).FindAction(look);
        jumpAction = playerControls.FindActionMap(playerActionMapName).FindAction(jump);
        fireAction = playerControls.FindActionMap(playerActionMapName).FindAction(fire);
        sprintAction = playerControls.FindActionMap(playerActionMapName).FindAction(sprint);

        escMenuAction = playerControls.FindActionMap(uiActionMapName).FindAction(escMenu);

        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        fireAction.performed += context => FireTriggered = true;
        fireAction.canceled += context => FireTriggered = false;

        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0f;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        fireAction.Enable();
        sprintAction.Enable();
    }

    private void OnDisable()
    {
        if (Instance == this)
        {
            moveAction.Disable();
            lookAction.Disable();
            jumpAction.Disable();
            fireAction.Disable();
            sprintAction.Disable();
        }
    }
}
