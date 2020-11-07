using UnityEngine;

public enum CurrentMiniGame
{
    ElectricityStand,
    LightBulb,
    Wiring
}

public class CameraMovement : MonoBehaviour
{
    public CurrentMiniGame currentMiniGame;
    public new GameObject camera;
    public float moveDistance;

    private void Start()
    {
        _MinigamesState.singleton.OpenMiniGame(currentMiniGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentMiniGame != CurrentMiniGame.ElectricityStand)
        {
            TryMoveRight();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentMiniGame != CurrentMiniGame.Wiring)
        {
            TryMoveLeft();
        }
    }


    private void TryMoveRight()
    {
        camera.transform.position = new Vector3(camera.transform.position.x + moveDistance, camera.transform.position.y, -40);

        if (currentMiniGame == CurrentMiniGame.Wiring)
        {
            currentMiniGame = CurrentMiniGame.LightBulb;
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
            currentMiniGame = CurrentMiniGame.LightBulb;
        }
        else
        {
            currentMiniGame = CurrentMiniGame.Wiring;
        }

        _MinigamesState.singleton.OpenMiniGame(currentMiniGame);
    }
}
