using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
    public bool isTurnedOff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Collider2D>();

        if (collider.CompareTag("Wrist") && _MinigamesState.singleton.standState == ElecricityStandState.Open)
        {
            isTurnedOff = !isTurnedOff;
            _MinigamesState.singleton.switchSwitcher(isTurnedOff);
        }
    }
}
