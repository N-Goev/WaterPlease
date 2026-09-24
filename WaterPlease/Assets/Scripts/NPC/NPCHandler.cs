using System.Collections;
using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    private float finalZ = -13.17f;
    private float moveSpeed = 4f;

    private void OnEnable()
    {
        EventBus.Add<DocumentStampedEvent>(OnDocumentStamped);
    }

    private void OnDisable()
    {
        EventBus.Remove<DocumentStampedEvent>(OnDocumentStamped);
    }

    private void Start()
    {
        StartCoroutine(MoveNPCCoroutine());
    }

    private void OnDocumentStamped(DocumentStampedEvent documentStampedEvent)
    {
        Destroy(gameObject);
    }

    IEnumerator MoveNPCCoroutine()
    {
        while (transform.position.z > finalZ)
        {
            Vector3 currentPos = transform.position;
            transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z - (moveSpeed * Time.deltaTime));

            yield return null;
        }

        EventBus.Invoke(new NPCReachedTableEvent());
    }
}
