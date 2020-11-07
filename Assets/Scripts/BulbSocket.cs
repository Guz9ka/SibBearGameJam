using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulbsocketState
{
    Broken,
    Empty,
    Repaired
}

public class BulbSocket : MonoBehaviour
{
    public BulbsocketState socketState;
    public int socketId;
    public float secondsToScrew;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Collider2D>();

        if (collider.CompareTag("Wrist"))
        {
            if(socketState == BulbsocketState.Broken)
            {
                StartCoroutine(UnscrewBulb());
            }
            else if(socketState == BulbsocketState.Empty)
            {
                StartCoroutine(ScrewInBulb());
            }
            else if(socketState == BulbsocketState.Repaired)
            {
                BreakBulb();
            }
        }
    }

    private IEnumerator UnscrewBulb()
    {
        yield return new WaitForSeconds(secondsToScrew);
        socketState = BulbsocketState.Empty;
    }

    private IEnumerator ScrewInBulb()
    {
        yield return new WaitForSeconds(secondsToScrew);
        socketState = BulbsocketState.Repaired;
        _MinigamesState.singleton.OnBulbRepaired(socketId, socketState);
    }

    private void BreakBulb()
    {
        //звук рыгания
        socketState = BulbsocketState.Broken;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Collider2D>();

        if (collider.CompareTag("Wrist"))
        {
            StopAllCoroutines();
            //прекратить проигрывать звук
        }
    }
}
