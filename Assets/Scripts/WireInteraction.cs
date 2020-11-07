using UnityEngine;

public enum WireInteractionState
{
    Static,
    InHand,
    PlugedIn
}

public class WireInteraction : MonoBehaviour
{
    public int wireId;
    public Transform wristTransform;
    public WireInteractionState InteractionState;

    [Header("Отрисовка линий")]
    public LineRenderer lineRenderer;

    [Header("Стартовые позиции проводов")]
    public Transform startPoint;

    private void Start()
    {
        SetDefaultWiresPosition();
    }

    private void Update()
    {
        UpdateLineSecondPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D collider = collision.GetComponent<Collider2D>();

        if (collider.CompareTag("Wrist"))
        {
            InteractionState = WireInteractionState.InHand;
        }
    }

    void UpdateLineSecondPosition()
    {
        if (InteractionState == WireInteractionState.InHand)
        {
            lineRenderer.SetPosition(1, wristTransform.position);
        }
    }

    public void SetWireEndPosition(Vector3 endPosition)
    {
        InteractionState = WireInteractionState.PlugedIn;
        lineRenderer.SetPosition(1, endPosition);
        _MinigamesState.singleton.OnWireRepaired(wireId);
    }

    public void SetDefaultWiresPosition()
    {
        if (InteractionState != WireInteractionState.PlugedIn)
        {
            InteractionState = WireInteractionState.Static;
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, new Vector2(startPoint.position.x + 1, startPoint.position.y));
        }
    }

}
