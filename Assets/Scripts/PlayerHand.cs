using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Rigidbody2D handRb;

    public float pushForceHorizontal;
    public float pushForceVertical;

    public float interpolateSpeedHorizontal;
    public float interpolateSpeedVertical;

    public float sensitivityMultiplierX;
    public float sensitivityMultiplierY;
    protected float mouseX;
    protected float mouseY;

    public float minPositionX;
    public float maxPositionX;
    public float minPositionY;
    public float maxPositionY;

    protected void Start()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        GetInput();
        MoveHand();
    }

    //Можно добавить больше садизма, запретив двигать рукой пока она в движении
    protected void GetInput()
    {
        mouseY = Input.GetAxis("Mouse Y");

        mouseX = Mathf.Lerp(mouseX, Input.GetAxis("Mouse X"), interpolateSpeedHorizontal * Time.deltaTime);
    }

    protected void MoveHand()
    {
        if (handRb.transform.localPosition.x > maxPositionX || handRb.transform.localPosition.x < minPositionX)
        {
            handRb.velocity = Vector2.zero;
        }

        Vector2 force = new Vector2(mouseX * pushForceHorizontal * sensitivityMultiplierX, mouseY * pushForceVertical * sensitivityMultiplierY);
        handRb.AddForce(force * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Collider2D>();

        if (collider.CompareTag("KeyHole"))
        {
            _MinigamesState.singleton.OpenElectricityStand();
        }
    }
}
