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

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void OnDocumentStamped(DocumentStampedEvent documentStampedEvent)
    {
        transform.position = startPosition;
    }

    private void OnStampDropped(ItemDroppedEvent itemDroppedEvent) {
        if(itemDroppedEvent.Item != gameObject) { return; }

        if(gameObject.transform.position.y < startPosition.y) {
            gameObject.transform.position = startPosition;
        }
    }
}
