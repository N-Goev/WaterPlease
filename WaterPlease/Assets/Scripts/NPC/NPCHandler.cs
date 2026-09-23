using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.Add<DocumentStampedEvent>(OnDocumentStamped);
    }

    private void OnDisable()
    {
        EventBus.Remove<DocumentStampedEvent>(OnDocumentStamped);
    }

    private void OnDocumentStamped(DocumentStampedEvent documentStampedEvent)
    {
        Destroy(gameObject);
    }
}
