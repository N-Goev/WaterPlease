using UnityEngine;

public class StampHandler : MonoBehaviour
{
    public enum StampType { Approve, Deny}

    [SerializeField] public StampType stampType;

    private Vector3 startPosition;

    private void OnEnable()
    {
        EventBus.Add<DocumentStampedEvent>(OnDocumentStamped);
    }

    private void OnDisable()
    {
        EventBus.Remove<DocumentStampedEvent>(OnDocumentStamped);
    }

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void OnDocumentStamped(DocumentStampedEvent documentStampedEvent)
    {
        transform.position = startPosition;
    }
}
