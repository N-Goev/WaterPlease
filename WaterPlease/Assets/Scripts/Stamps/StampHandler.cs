using UnityEngine;

public class StampHandler : MonoBehaviour
{
    public enum StampType { Approve, Deny}

    [SerializeField] public StampType stampType;

    private Vector3 startPosition;

    private void OnEnable()
    {
        EventBus.Add<DocumentStampedEvent>(OnDocumentStamped);
        EventBus.Add<ItemDroppedEvent>(OnStampDropped);
    }

    private void OnDisable()
    {
        EventBus.Remove<DocumentStampedEvent>(OnDocumentStamped);
        EventBus.Remove<ItemDroppedEvent>(OnStampDropped);
    }

    private void Update()
    {
        CheckBoundariesHorizontal();
    }

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void ResetPosition()
    {
        transform.position = startPosition;
    }

    private void OnDocumentStamped(DocumentStampedEvent documentStampedEvent)
    {
        ResetPosition();
    }

    private void OnStampDropped(ItemDroppedEvent itemDroppedEvent) {
        if(itemDroppedEvent.Item != gameObject) { return; }

        CheckBoundariesVertical();
        CheckBoundariesHorizontal();
    }

    private void CheckBoundariesVertical()
    {
        if (gameObject.transform.position.y < startPosition.y)
        {
            ResetPosition();
        }
    }

    private void CheckBoundariesHorizontal()
    {
        if (gameObject.transform.position.x < -1.2f)
        {
            ResetPosition();
        }
        else if (gameObject.transform.position.x > 1.15f)
        {
            ResetPosition();
        }
    }
}
