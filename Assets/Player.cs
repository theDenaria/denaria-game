using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;

    public string playerId;

    private float lastRotation;

    private void Start()
    {
        lastRotation = transform.rotation.eulerAngles.y;
        gameManager = GameManager.Instance;

    }

    private void Update()
    {
        if (gameManager.ownPlayerId == playerId && Mathf.Abs(transform.rotation.eulerAngles.y - lastRotation) > 1.0)
        {
            gameManager.SendRotationToServer(transform.rotation.eulerAngles.y);
            lastRotation = transform.rotation.eulerAngles.y;
        }
    }


    public void SetPlayerId(string id)
    {
        playerId = id;
    }




}
