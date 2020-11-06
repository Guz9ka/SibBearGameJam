using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Rigidbody2D rb;

    public float horizontalForce;
    float horizontalInput;

    void Start()
    {
        
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
