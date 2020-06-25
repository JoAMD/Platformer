using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffect : MonoBehaviour
{
    private float waitTime = 0.5f;
    private PlatformEffector2D PlatformEffector2;
    // Start is called before the first frame update
    void Start()
    {
        PlatformEffector2 = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (waitTime <= 0)
            {
                PlatformEffector2.rotationalOffset = 180f;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            PlatformEffector2.rotationalOffset = 0f;
            Debug.Log("key up");
        }
    }
}
