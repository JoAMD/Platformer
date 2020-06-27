using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static event Action OnInteract; 

    private void Update()
    {
        GetInteractInput();
    }

    private void GetInteractInput()
    {
        if (Input.GetButtonDown("Interact"))
        {
            //Calls all functions subscribed to the OnInteract action. Currently only elevator
            OnInteract?.Invoke();
        }
    }
}
