using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour singleton { get; private set; }

    public Rigidbody2D rb;

    public float horizontalForce;
    float horizontalInput;

    void Start()
    {
        singleton = this;
    }

    void Update()
    {
        GetInput();
        Move();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void Move()
    {
        Vector2 force = new Vector2(horizontalForce * horizontalInput, 0);
        rb.AddForce(force * Time.deltaTime);
    }
}
