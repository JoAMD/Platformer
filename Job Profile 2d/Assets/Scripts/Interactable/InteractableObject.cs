using UnityEngine;

public abstract class InteractableObject : PlayerDetector, IInteractable
{

    protected virtual void OnEnable()
    {
        //Debug.Log("Added on interact to action");
        InputController.OnInteract += OnInteract;
    }

    protected virtual void OnDestroy()
    {
        //Debug.Log("Removed on interact from action");
        InputController.OnInteract -= OnInteract;
    }


    /// <summary>
    /// OnInteract function runs when player presses the Interact Button
    /// </summary>
    public abstract void OnInteract();

}
