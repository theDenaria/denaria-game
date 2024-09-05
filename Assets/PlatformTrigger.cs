using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    public GameObject canvasMenu; // Reference to the canvas menu

    // Called when another object enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate the canvas menu
            canvasMenu.SetActive(true);
        }
    }

    // Called when another object exits the trigger collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Deactivate the canvas menu
            canvasMenu.SetActive(false);
        }
    }
}
