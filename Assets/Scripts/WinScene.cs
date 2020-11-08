using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScene : MonoBehaviour
{
    public static WinScene singleton { get; private set; }
    public List<GameObject> toDeactivate;
    public GameObject winScene;
    public BelkaGriefing belkaGriefing;

    void Start()
    {
        singleton = this;
    }

    public void OnGameEnd()
    {
        foreach (var obj in toDeactivate)
        {
            obj.SetActive(false);
        }

        belkaGriefing.griefAvailable = false;
        PlayerMovement.singleton.StopAllCoroutines();
        PlayerMovement.singleton.timeBetweenBurp = 100000;

        winScene.SetActive(true);
    }
}
