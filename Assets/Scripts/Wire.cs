using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    Vector2 mousePosition;
    private Color currentLineColor;
    private bool wireInHand;
    public Transform wristTransoform;

    [Header("Отрисовка линий")]
    public LineRenderer lineRendererRed;
    public LineRenderer lineRendererGreen;
    public LineRenderer lineRendererBlue;

    [Header("Стартовые позиции проводов")]
    public Transform startPositionRed;
    public Transform startPositionGreen;
    public Transform startPositionBlue;

    [Header("Финальные позиции проводов")]
    public Transform endPositionRed;
    public Transform endPositionGreen;
    public Transform endPositionBlue;

    private void Start()
    {
        SetDefaultWiresPosition();
    }

    private void Update()
    {
        GetInput();
        TryGetWireColor();
        UpdateLineSecondPosition();
    }

    void GetInput()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void TryGetWireColor()
    {
        

        if()
        {
            switch (hitInfo.collider.tag)
            {
                case "StartPointRed":
                    currentLineColor = Color.red;
                    break;
                case "StartPointGreen":
                    currentLineColor = Color.green;
                    break;
                case "StartPointBlue":
                    currentLineColor = Color.blue;
                    break;
            }
        }
    }

    void UpdateLineSecondPosition()
    {
        if(currentLineColor == Color.red)
        {
            
        }
        else if(currentLineColor == Color.green)
        {
            lineRendererGreen.SetPosition(1, wristPosition);
        }
        else if(currentLineColor == Color.blue)
        {
            lineRendererBlue.SetPosition(1, wristPosition);
        }
    }

    void SetDefaultWiresPosition()
    {
        lineRendererRed.SetPosition(0, startPositionRed.position);
        lineRendererGreen.SetPosition(0, startPositionGreen.position);
        lineRendererBlue.SetPosition(0, startPositionBlue.position);

        lineRendererRed.SetPosition(1, new Vector2(startPositionRed.position.x + 1, startPositionRed.position.y));
        lineRendererGreen.SetPosition(1, new Vector2(startPositionGreen.position.x + 1, startPositionGreen.position.y));
        lineRendererBlue.SetPosition(1, new Vector2(startPositionBlue.position.x + 1, startPositionBlue.position.y));
    }
}
