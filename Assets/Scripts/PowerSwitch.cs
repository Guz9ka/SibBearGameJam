using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
    public bool isTurnedOff = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Collider2D>();

        if (collider.CompareTag("Wrist"))
        {
            isTurnedOff = !isTurnedOff;
            _MinigamesState.singleton.switchSwitcher(isTurnedOff);
        }
    }
}
