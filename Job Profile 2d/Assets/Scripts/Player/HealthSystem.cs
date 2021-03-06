﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem sRef;
    private void Awake()
    {
        sRef = this;
    }

    [SerializeField] private int health = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
