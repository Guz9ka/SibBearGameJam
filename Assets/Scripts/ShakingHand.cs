using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingHand : MonoBehaviour
{
    public Rigidbody2D handRb;

    public float pushForceHorizontal;
    public float pushForceVertical;

    public float interpolateSpeedHorizontal;
    public float interpolateSpeedVertical;

    public float sensitivityMultiplierX;
    public float sensitivityMultiplierY;
    float mouseX;
    float mouseY;

    public float minPositionX;
    public float maxPositionX;
    public float minPositionY;
    public float maxPositionY;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        ClampHandPosition();
        GetInput();
        MoveHand();
    }

    private void ClampHandPosition()
    {
        float clampedPositionX;
        float clampedPositionY;

        clampedPositionX = Mathf.Clamp(handRb.transform.localPosition.x, minPositionX, maxPositionX);
        clampedPositionY = Mathf.Clamp(handRb.transform.localPosition.y, minPositionY, maxPositionY);

        handRb.transform.localPosition = new Vector2(clampedPositionX, clampedPositionY);
    }

    //Можно добавить больше садизма, запретив двигать рукой пока она в движении
    private void GetInput()
    {
        mouseY = Input.GetAxis("Mouse Y"); //Mathf.Lerp(mouseY, Input.GetAxis("Mouse Y"), interpolateSpeedVertical * Time.deltaTime);

        if (Input.GetAxis("Mouse X") < 0.1f && Input.GetAxis("Mouse X") > 0.1f)
        {
            mouseX = Mathf.Lerp(mouseX, Input.GetAxis("Mouse X"), interpolateSpeedHorizontal * Time.deltaTime); //Input.GetAxis("Mouse X");
        }
        else
        {
            mouseX = Input.GetAxis("Mouse X");
        }
    }

    private void MoveHand()
    {
        if (handRb.transform.localPosition.x > maxPositionX || handRb.transform.localPosition.x < minPositionX)
        {
            handRb.velocity = Vector2.zero;
        }

        Vector2 force = new Vector2(mouseX * pushForceHorizontal * sensitivityMultiplierX, mouseY * pushForceVertical * sensitivityMultiplierY);
        handRb.AddForce(force * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("KeyHole"))
        {
            
        }
    }
}
