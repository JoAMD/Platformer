using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBehaviour : InteractableObject
{
    [SerializeField] private bool isPlayerInRange = false;

    public override void OnInteract()
    {
        Debug.Log("running on interact");
        if (isPlayerInRange)
        {
            Debug.Log("Player started elevator");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInRange = false;
    }

}
