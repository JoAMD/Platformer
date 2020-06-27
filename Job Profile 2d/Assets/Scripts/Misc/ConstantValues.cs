using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantValues : MonoBehaviour
{
    public static ConstantValues instance;
    private void Awake()
    {
        instance = this;
    }

    public readonly string TAG_PLAYER = "Player";

}
