using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : InteractableObject
{

    public KeyBehaviour keyBehaviour;
    private int keyID;
    public SpriteRenderer spriteRend;
    public float yUpDist = 3;
    public float doorOpenSmooth = 1f;
    public Collider2D doorCollider2D;
    public bool isCloseAfterDelay = false;
    public float closeDelay = 2f;
    public bool isUp = true;

    public override void OnInteract()
    {
        if (isPlayerInRange && Inventory.instance.HasKey(keyID, out int keyInvIdx))
        {
            if (!isCloseAfterDelay)
            {
                //key removed
                Inventory.instance.RemoveKey(keyInvIdx);
            }

            spriteRend.color = Color.white;
            StartCoroutine(OpenDoor());
            Debug.Log("Opening Door");
            doorCollider2D.enabled = false;
        }
    }

    private void Start()
    {
        keyID = keyBehaviour.gameObject.GetHashCode();
        Debug.Log("in doorscript => " + keyID);
        StartCoroutine(CheckForKeyMakeDoorGreen());
    }

    private IEnumerator OpenDoor()
    {
        float t = 0;
        float yCurr = transform.position.y;
        float yEnd = yCurr + yUpDist;
        while (t < 1)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(yCurr, yEnd, t));
            t += Time.deltaTime * doorOpenSmooth;
            yield return new WaitForEndOfFrame();
        }
        transform.position = new Vector3(transform.position.x, yEnd);

        if (isCloseAfterDelay)
        {
            StartCoroutine(CloseDoor());
        }

    }

    private IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(closeDelay);

        float t = 0;
        float yCurr = transform.position.y;
        float yEnd = yCurr - yUpDist;
        while (t < 1)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(yCurr, yEnd, t));
            t += Time.deltaTime * doorOpenSmooth;
            yield return new WaitForEndOfFrame();
        }
        transform.position = new Vector3(transform.position.x, yEnd);
        doorCollider2D.enabled = true;

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
