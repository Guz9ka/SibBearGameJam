using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Walk,
    Minigame
}

public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour singleton { get; private set; }

    public Rigidbody2D rb;

    public float horizontalForce;
    float horizontalInput;

    [Header("Поиск мини игр")]
    public float checkRadius;

    void Start()
    {
        singleton = this;
    }

    void Update()
    {
        StartMinigame();
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

    void StartMinigame()
    {
        var collider = Physics2D.OverlapCircle(gameObject.transform.position, checkRadius, LayerMask.GetMask("MiniGame"));


    }
}
