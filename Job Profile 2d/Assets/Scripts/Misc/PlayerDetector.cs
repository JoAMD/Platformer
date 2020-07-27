using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    [SerializeField] protected bool isPlayerInRange = false;

    /// <summary>
    /// player in range true
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("in" + name);
        if (collision.CompareTag(ConstantValues.instance.TAG_PLAYER_DETECT))
        {
        Debug.Log("in,mmmmmmmmm" + gameObject.name);
            isPlayerInRange = true;
        }
    }

    /// <summary>
    /// player in range false
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(ConstantValues.instance.TAG_PLAYER_DETECT))
        {
            isPlayerInRange = false;
        }
    }
}
