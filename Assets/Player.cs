using System;
using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour
{
    // private CharacterController characterController;
    // private Camera mainCamera;
    // private float verticalRotation;

    private GameManager gameManager;

    public string playerId;

    private float lastRotation;

    private void Start()
    {
        lastRotation = transform.rotation.eulerAngles.y;
        gameManager = FindObjectOfType<GameManager>();

    }

    private void Update()
    {
        Debug.Log($"ownplayerid: {gameManager.ownPlayerId}");
        Debug.Log($"playerId: {playerId}");
        Debug.Log($"transform.rotation.y: {transform.rotation.eulerAngles.y}");
        Debug.Log($"lastRotation: {lastRotation}");

        if (gameManager.ownPlayerId == playerId && Mathf.Abs(transform.rotation.eulerAngles.y - lastRotation) > 1.0)
        {
            Debug.Log("Player Rotated");
            gameManager.SendRotationToServer(transform.rotation.eulerAngles.y);
            lastRotation = transform.rotation.eulerAngles.y;
        }
    }


    public void SetPlayerId(string id)
    {
        playerId = id;
    }




}
