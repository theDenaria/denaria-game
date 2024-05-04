using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class DisconnectButton : MonoBehaviour
{
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void DisconnectClicked()
    {
        gameManager.DisconnectButtonClicked();
    }
}
