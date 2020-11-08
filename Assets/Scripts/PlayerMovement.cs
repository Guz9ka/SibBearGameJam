using System.Collections;
using UnityEngine;

public enum CurrentMiniGame
{
    ElectricityStand,
    Luster,
    Wiring
}

public enum PlayerState
{
    Dead,
    Alive
}

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement singleton { get; private set; }

    public PlayerState playerState;
    public CurrentMiniGame currentMiniGame;
    public new GameObject camera;
    public float moveDistance;

    public float timeBetweenBurp;
    private bool burpAvailable = true;

    private void Start()
    {
        singleton = this;
        _MinigamesState.singleton.OpenMiniGame(currentMiniGame);
    }

    private void Update()
    {
        if (burpAvailable)
        {
            StartCoroutine(Burp());
        }

        if (playerState == PlayerState.Alive)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && currentMiniGame != CurrentMiniGame.ElectricityStand)
            {
                TryMoveRight();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && currentMiniGame != CurrentMiniGame.Wiring)
            {
                TryMoveLeft();
            }
        }
    }

    IEnumerator Burp()
    {
        burpAvailable = false;

        yield return new WaitForSeconds(timeBetweenBurp);

        SoundsPlayer.singleton.PlaySoundBurp();
        burpAvailable = true;
    }

    private void TryMoveRight()
    {
        camera.transform.position = new Vector3(camera.transform.position.x + moveDistance, camera.transform.position.y, -40);

        if (currentMiniGame == CurrentMiniGame.Wiring)
        {
            currentMiniGame = CurrentMiniGame.Luster;
        }
        else
        {
            currentMiniGame = CurrentMiniGame.ElectricityStand;
        }

        _MinigamesState.singleton.OpenMiniGame(currentMiniGame);
    }

    private void TryMoveLeft()
    {
        camera.transform.position = new Vector3(camera.transform.position.x - moveDistance, camera.transform.position.y, -40);

        if (currentMiniGame == CurrentMiniGame.ElectricityStand)
        {
            currentMiniGame = CurrentMiniGame.Luster;
        }
        else
        {
            currentMiniGame = CurrentMiniGame.Wiring;
        }

        _MinigamesState.singleton.OpenMiniGame(currentMiniGame);
    }
}
