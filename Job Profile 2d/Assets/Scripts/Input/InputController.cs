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
            OnInteract?.Invoke();
        }
    }
}
