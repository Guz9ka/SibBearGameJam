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

    public bool[] taskStates;
    public float taskAutoCloseDelay;

    [Header("Электрощиток закрытый")]
    public ElecricityStandState standState = ElecricityStandState.Closed;

    public List<GameObject> toActivateStandClosed;
    public List<GameObject> toDeactivateStandClosed;

    [Header("Электрощиток открытый")]
    public int switchesActive = 0;

    public List<GameObject> toActivateStandOpen;
    public List<GameObject> toDeactivateStandOpen;

    [Header("Люстра")]
    private BulbsocketState[] socketsStates;
    public List<GameObject> toActivateLusterActive;
    public List<GameObject> toDeactivateLusterActive;

    [Header("Починка проводки")]
    private WireInteractionState[] wireInteractionStates;
    public List<GameObject> toActivateWiringActive;
    public List<GameObject> toDeactivateWiringActive;

    private void Start()
    {
        singleton = this;
        taskStates = new bool[3];
        socketsStates = new BulbsocketState[3];
        wireInteractionStates = new WireInteractionState[3];
    }

    #region Взаимодействие со сценой
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

    public void OpenMiniGame(CurrentMiniGame currentMiniGame)
    {
        switch (currentMiniGame)
        {
            case CurrentMiniGame.ElectricityStand:
                ActivateElecticityStand();
                break;
            case CurrentMiniGame.Luster:
                ActivateLuster();
                break;
            case CurrentMiniGame.Wiring:
                ActivateWiring();
                break;
        }
    }
    #endregion

    #region Электрощиток
    public void ActivateElecticityStand()
    {
        if (standState == ElecricityStandState.Closed)
        {
            SwitchObjectStates(toActivateStandClosed, toDeactivateStandClosed);
        }
        else if (standState == ElecricityStandState.Open)
        {
            SwitchObjectStates(toActivateStandOpen, toDeactivateStandOpen);
        }
        else if (standState == ElecricityStandState.TurnedOff)
        {
            SwitchObjectStates(toActivateStandOpen, toDeactivateStandOpen);
        }
    }

    public void OpenElectricityStand()
    {
        SoundsPlayer.singleton.PlaySoundOpenElectricityStand();
        standState = ElecricityStandState.Open;
        SwitchObjectStates(toActivateStandOpen, toDeactivateStandOpen);
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

        CheckElectricityStandRepaired();
    }

    public void CheckElectricityStandRepaired()
    {
        if (switchesActive >= 3)
        {
            standState = ElecricityStandState.TurnedOff;
            taskStates[0] = true;
            StartCoroutine(CheckAllTasksComplete());
        }
        else
        {
            standState = ElecricityStandState.Open;
        }
    }
    #endregion

    #region Починка люстры
    void ActivateLuster()
    {
        SwitchObjectStates(toActivateLusterActive, toDeactivateLusterActive);
    }

    public void OnBulbRepaired(int socketId, BulbsocketState socketState)
    {
        socketsStates[socketId] = socketState;
        CheckLusterRepaired();
    }

    void CheckLusterRepaired()
    {
        if (socketsStates[0] == BulbsocketState.Repaired && socketsStates[1] == BulbsocketState.Repaired && socketsStates[2] == BulbsocketState.Repaired)
        {
            taskStates[1] = true; //Вторая задача выполнена
            StartCoroutine(CheckAllTasksComplete());
        }
        else
        {
            StopAllCoroutines();
        }
    }
    #endregion

    #region Починка проводки
    void ActivateWiring()
    {
        SwitchObjectStates(toActivateWiringActive, toDeactivateWiringActive);
    }

    public void OnWireRepaired(int wireId)
    {
        wireInteractionStates[wireId] = WireInteractionState.PlugedIn;

        CheckWireRepaired();
    }

    public void CheckWireRepaired()
    {
        if (wireInteractionStates[0] == WireInteractionState.PlugedIn && wireInteractionStates[1] == WireInteractionState.PlugedIn && wireInteractionStates[2] == WireInteractionState.PlugedIn)
        {
            taskStates[2] = true;
            StartCoroutine(CheckAllTasksComplete());
        }
        else
        {
            StopAllCoroutines();
        }
    }
    #endregion

    IEnumerator CheckAllTasksComplete()
    {
        yield return new WaitForSeconds(taskAutoCloseDelay);

        CheckElectricityStandRepaired();
        CheckLusterRepaired();
        CheckWireRepaired();

        if (taskStates[0] == true && taskStates[1] == true && taskStates[2] == true) //Проверить, все ли задачи выполнены
        {
            Debug.Log("Game end");
        }
    }
}
