using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelkaGriefing : MonoBehaviour
{
    //появиться
    //испортить вещь
    //дождаться прихода игрока
    //исчезнуть
    private bool isVanishing = false;
    public bool griefAvailable = false;

    public float delayBetweenGrief;
    public float delayBeforeVanish;

    public GameObject belkaElectricityStand;
    public GameObject belkaLuster;
    public GameObject belkaWiring;

    public List<PowerSwitch> powerSwitches;
    public List<BulbSocket> bulbSockets;
    public List<WireInteraction> wires;

    private void Update()
    {
        CheckSpottedState();
        if (griefAvailable) StartCoroutine(StartGrief());
    }

    IEnumerator StartGrief()
    {
        griefAvailable = false;
        ChooseObjectToGrief();

        yield return new WaitForSeconds(delayBetweenGrief);

        griefAvailable = true;
    }

    void ChooseObjectToGrief()
    {
        int griefId = Random.Range(0, 2);

        switch (griefId)
        {
            case 0:
                TurnOffSwitch();
                break;
            case 1:
                BreakBulb();
                break;
            case 2:
                UnPlugWire();
                break;
        }
    }

    void TurnOffSwitch()
    {
        belkaElectricityStand.SetActive(true);
        int griefTargetId = Random.Range(0, powerSwitches.Count - 1);

        powerSwitches[griefTargetId].SwitchSwitch();
    }

    void BreakBulb()
    {
        belkaLuster.SetActive(true);
        int griefTargetId = Random.Range(0, bulbSockets.Count - 1);

        bulbSockets[griefTargetId].BreakBulb();
    }

    void UnPlugWire()
    {
        belkaWiring.SetActive(true);
        int griefTargetId = Random.Range(0, wires.Count - 1);

        wires[griefTargetId].InteractionState = WireInteractionState.Static;
        wires[griefTargetId].lineRenderer.SetPosition(0, wires[griefTargetId].startPoint.position);
        wires[griefTargetId].lineRenderer.SetPosition(1, new Vector3(wires[griefTargetId].startPoint.position.x + 1, wires[griefTargetId].startPoint.position.y, 10));
    }

    void CheckSpottedState()
    {
        if (isVanishing == false)
        {
            if (PlayerMovement.singleton.currentMiniGame == CurrentMiniGame.ElectricityStand && belkaElectricityStand.activeSelf == true)
            {
                StartCoroutine(BelkaVanish(belkaElectricityStand));
            }
            else if (PlayerMovement.singleton.currentMiniGame == CurrentMiniGame.Luster && belkaLuster.activeSelf == true)
            {
                StartCoroutine(BelkaVanish(belkaLuster));
            }
            else if (PlayerMovement.singleton.currentMiniGame == CurrentMiniGame.Wiring && belkaWiring.activeSelf == true)
            {
                StartCoroutine(BelkaVanish(belkaWiring));
            }
        }
    }

    IEnumerator BelkaVanish(GameObject belka)
    {
        isVanishing = true;

        yield return new WaitForSeconds(delayBeforeVanish);

        belka.SetActive(false);
        isVanishing = false;
    }
}
