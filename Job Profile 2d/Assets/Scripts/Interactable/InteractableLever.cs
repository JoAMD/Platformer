using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLever : InteractableObject
{
    public InteractableDoorBehaviour doorBehaviour;

    public override void OnInteract()
    {
        if (isPlayerInRange)
        {
            if (doorBehaviour.isOpen)
            {
                StartCoroutine(doorBehaviour.CloseDoor(false));
            }
            else
            {
                StartCoroutine(doorBehaviour.OpenDoor());
            }
        }
    }
}
