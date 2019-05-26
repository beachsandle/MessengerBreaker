﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.rotation*Vector2.right*GameManager.BallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
