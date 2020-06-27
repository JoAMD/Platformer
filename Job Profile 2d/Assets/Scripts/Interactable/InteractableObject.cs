using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    protected virtual void OnEnable()
    {
        Debug.Log("Added on interact to action");
        InputController.OnInteract += OnInteract;
    }

    protected virtual void OnDestroy()
    {
        Debug.Log("Removed on interact from action");
        InputController.OnInteract -= OnInteract;
    }

    public abstract void OnInteract();
}
