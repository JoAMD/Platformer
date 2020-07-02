using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : InteractableObject
{
    private void Update()
    {
        if (isPlayerInRange)
        {
            Inventory.instance.AddItem(new Item(gameObject.GetHashCode()));
            gameObject.SetActive(false);
        }
    }

    public override void OnInteract()
    {
    }
}
