using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElecricityStandState
{
    Closed,
    Open,
    TurnedOff
}

public class _MinigamesState : MonoBehaviour
{
    public static _MinigamesState singleton { get; private set; }

    private void Start()
    {
        singleton = this;
    }
}
