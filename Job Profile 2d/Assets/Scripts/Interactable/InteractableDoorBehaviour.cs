using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    None,
    Key,
    Lever,
    Both
}

public class InteractableDoorBehaviour : InteractableObject
{
    public DoorType doorType = DoorType.Key;
    public KeyBehaviour keyBehaviour;
    private int keyID;
    public SpriteRenderer spriteRend;
    public float yUpDist = 3;
    public float doorOpenSmooth = 1f;
    public Collider2D interactableCollider2D;
    public bool isCloseAfterDelay = false;
    public float closeDelay = 2f;
    public bool isUp = true;
    public Transform doorTransform;
    [HideInInspector] public bool isOpen = false;

    public override void OnInteract()
    {
        if (isPlayerInRange)
        {
            StartCoroutine(OpenDoor());
        }
    }

    private void Start()
    {
        if (doorType == DoorType.Key ||
            doorType == DoorType.Both)
        {
            keyID = keyBehaviour.gameObject.GetHashCode();
            Debug.Log("in doorscript => " + keyID);
            StartCoroutine(CheckForKeyMakeDoorGreen());
        }
    }

    public IEnumerator OpenDoor()
    {
        if (doorType == DoorType.Key || doorType == DoorType.Both)
        {
            if (Inventory.instance.HasKey(keyID, out int keyInvIdx))
            {
                if (isCloseAfterDelay || doorType == DoorType.Both)
                {
                    //dont remove key but keep it for later use
                }
                else
                {
                    //key removed
                    Inventory.instance.RemoveKey(keyInvIdx);
                }

                spriteRend.color = Color.white;
                Debug.Log("Opening Door");
            }
            else
            {
                yield break;
            }
        }

        interactableCollider2D.enabled = false;
        float t = 0;
        float yCurr = doorTransform.position.y;
        float yEnd = yCurr + yUpDist;
        while (t < 1)
        {
            doorTransform.position = new Vector3(doorTransform.position.x, Mathf.Lerp(yCurr, yEnd, t));
            t += Time.deltaTime * doorOpenSmooth;
            yield return new WaitForEndOfFrame();
        }
        doorTransform.position = new Vector3(doorTransform.position.x, yEnd);

        isOpen = true;

        if (isCloseAfterDelay)
        {
            StartCoroutine(CloseDoor(true));
        }

        if (doorType == DoorType.Lever || doorType == DoorType.Both)
        {
            interactableCollider2D.enabled = true;
        }


    }

    public IEnumerator CloseDoor(bool isDelay)
    {
        interactableCollider2D.enabled = false;
        if (isDelay)
        {
            yield return new WaitForSeconds(closeDelay);
        }

        float t = 0;
        float yCurr = doorTransform.position.y;
        float yEnd = yCurr - yUpDist;
        while (t < 1)
        {
            doorTransform.position = new Vector3(doorTransform.position.x, Mathf.Lerp(yCurr, yEnd, t));
            t += Time.deltaTime * doorOpenSmooth;
            yield return new WaitForEndOfFrame();
        }
        doorTransform.position = new Vector3(doorTransform.position.x, yEnd);
        interactableCollider2D.enabled = true;
        
        isOpen = false;

    }

    private IEnumerator CheckForKeyMakeDoorGreen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (Inventory.instance.HasKey(keyID, out int keyInvIdx))
            {
                spriteRend.color = Color.green;
                break;
            }
        }
    }
}
