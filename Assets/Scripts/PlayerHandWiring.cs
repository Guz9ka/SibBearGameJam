using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandWiring : PlayerHand
{
    public WireInteraction wireRed;
    public WireInteraction wireGreen;
    public WireInteraction wireBlue;

    bool wireInHandRed;
    bool wireInHandGreen;
    bool wireInHandBlue;

    protected override void FixedUpdate()
    {
        ClampHandPosition();
        GetInput();
        MoveHand();
        CheckWiresInteractionState();
        CheckWiresCountInHand();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Collider2D>();
        TryPlugInWire(collider);
    }

    void CheckWiresInteractionState()
    {
        wireInHandRed = wireRed.InteractionState == WireInteractionState.InHand;
        wireInHandGreen = wireGreen.InteractionState == WireInteractionState.InHand;
        wireInHandBlue = wireBlue.InteractionState == WireInteractionState.InHand;
    }

    void CheckWiresCountInHand()
    {
        if (wireInHandRed && (wireInHandGreen || wireInHandBlue))
        {
            ResetWiresPositions();
        }
        else if(wireInHandGreen && (wireInHandRed || wireInHandBlue))
        {
            ResetWiresPositions();
        }
        else if(wireInHandBlue && (wireInHandGreen || wireInHandRed))
        {
            ResetWiresPositions();
        }
    }

    protected void TryPlugInWire(Collider2D collider)
    {
        if(collider.CompareTag("EndPointRed") || collider.CompareTag("EndPointGreen") || collider.CompareTag("EndPointBlue"))
        {
            if (collider.CompareTag("EndPointRed") && wireInHandRed)
            {
                wireRed.SetWireEndPosition(collider.transform.position);
            }
            else if (collider.CompareTag("EndPointGreen") && wireInHandGreen)
            {
                wireGreen.SetWireEndPosition(collider.transform.position);
            }
            else if (collider.CompareTag("EndPointBlue") && wireInHandBlue)
            {
                wireBlue.SetWireEndPosition(collider.transform.position);
            }
            else
            {
                ResetWiresPositions();
            }
        }
    }

    private void ResetWiresPositions()
    {
        wireRed.SetDefaultWiresPosition();
        wireGreen.SetDefaultWiresPosition();
        wireBlue.SetDefaultWiresPosition();
    }
}
