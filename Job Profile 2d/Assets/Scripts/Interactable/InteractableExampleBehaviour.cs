using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableExampleBehaviour : InteractableObject
{
    public override void OnInteract()
    {
        if (isPlayerInRange)
        {
            DoInteractingStuff();
        }
    }

    /// <summary>
    /// OnInteract function runs when player presses the Interact Button
    /// CHecks for player in range, then move elevator
    /// isPlayerInRange is set in base class InteractableObject (using Player tag as well)
    /// </summary>
    private void DoInteractingStuff()
    {
        Debug.Log("Player has interacted");
    }
}
