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

    [Header("Электрощиток закрытый")]
    public ElecricityStandState standState = ElecricityStandState.Closed;

    public List<GameObject> toActivateStandClosed;
    public List<GameObject> toDeactivateStandClosed;

    [Header("Электрощиток открытый")]
    public int switchesActive = 0;

    public List<GameObject> toActivateStandOpen;
    public List<GameObject> toDeactivateStandOpen;

    private void Start()
    {
        singleton = this;
    }

    void SwitchObjectStates(List<GameObject> toActivate, List<GameObject> toDeactivate)
    {
        foreach (var obj in toActivate)
        {
            obj.SetActive(true);
        }

        foreach(var obj in toDeactivate)
        {
            obj.SetActive(false);
        }
    }

    #region Электрощиток
    public void ActivateElecticityShield()
    {
        if (standState == ElecricityStandState.Closed)
        {
            SwitchObjectStates(toActivateStandClosed, toDeactivateStandClosed);
        }
        else if (standState == ElecricityStandState.Open)
        {
            SwitchObjectStates(toActivateStandOpen, toDeactivateStandClosed);
        }
        else if (standState == ElecricityStandState.TurnedOff)
        {
            SwitchObjectStates(toActivateStandOpen, toDeactivateStandClosed);
        }
    }

    public void OpenElectricityStand()
    {
        standState = ElecricityStandState.Open;
        SwitchObjectStates(toActivateStandOpen, toDeactivateStandClosed);
    }

    public void switchSwitcher(bool turnedOn)
    {
        switch (turnedOn)
        {
            case true:
                switchesActive += 1;
                break;
            case false:
                switchesActive -= 1;
                break;
        }
    }
    #endregion

}
