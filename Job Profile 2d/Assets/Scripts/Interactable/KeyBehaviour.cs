using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : InteractableObject
{
    public override void OnInteract()
    {
        Inventory.instance.AddItem(new Item(gameObject.GetHashCode()));
        gameObject.SetActive(false);
    }
}
