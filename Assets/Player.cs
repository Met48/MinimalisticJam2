﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    public float speed = 2f;
    public float jumpForce = 600f;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        var input = Input.GetAxis("Horizontal");
        var movement = input * speed;

        rb.velocity = new Vector3(movement, rb.velocity.y, 0);

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && rb.velocity.y == 0) // If the player hits up while their vertical velocity is 0, then jump.
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
	}
}
