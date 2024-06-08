using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public float xAxis, yAxis = 0f;
    [SerializeField] Transform camFollowPos;
    [SerializeField] float mouseSensitivity = 0.2f;

    private PlayerInputHandler playerInputHandler;
    // Start is called before the first frame update
    void Start()
    {
        playerInputHandler = PlayerInputHandler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        var lookInput = playerInputHandler.LookInput;
        if (lookInput != Vector2.zero)
        {
            xAxis += lookInput.x * mouseSensitivity;
            yAxis -= lookInput.y * mouseSensitivity;
            yAxis = Mathf.Clamp(yAxis, -80, 80);
        }

    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }
}
