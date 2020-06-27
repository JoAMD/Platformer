using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] protected bool isPlayerInRange = false;

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

    /// <summary>
    /// player in range true
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ConstantValues.instance.TAG_PLAYER))
        {
            isPlayerInRange = true;
        }
    }

    /// <summary>
    /// player in range false
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(ConstantValues.instance.TAG_PLAYER))
        {
            isPlayerInRange = false;
        }
    }

}
