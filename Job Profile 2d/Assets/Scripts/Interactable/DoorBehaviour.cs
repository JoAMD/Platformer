using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : InteractableObject
{

    public KeyBehaviour keyBehaviour;
    private int keyID;
    public SpriteRenderer spriteRend;
    public float yUpDist = 3;
    private float yCurr;
    public float doorOpenSmooth = 1f;
    public Collider2D collider2D;

    public override void OnInteract()
    {
        if (Inventory.instance.HasKey(keyID, out int keyInvIdx))
        {
            //key removed
            Inventory.instance.RemoveKey(keyInvIdx);

            spriteRend.color = Color.white;
            StartCoroutine(OpenDoor());
            Debug.Log("Opening Door");
            collider2D.enabled = false;
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
        yCurr = transform.position.y;
        float yEnd = yCurr + yUpDist;
        while (t < 1)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(yCurr, yEnd, t));
            t += Time.deltaTime * doorOpenSmooth;
            yield return new WaitForEndOfFrame();
        }
        transform.position = new Vector3(transform.position.x, yEnd);
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
