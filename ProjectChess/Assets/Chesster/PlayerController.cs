﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Movement Movement;

    private void Awake()
    {
        if (Movement == null)
        {
            Movement = GetComponent<Movement>();
        }

        Global.GameManager.Player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
