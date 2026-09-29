using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCHandler : MonoBehaviour, IPointerDownHandler
{
    private float finalZ = -7f; 
    private float moveSpeed = 4f;

    NPCScriptableObject npcScriptableObject;

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

    public void InitNPC(NPCScriptableObject npcScriptableObject)
    {
        this.npcScriptableObject = npcScriptableObject;
        GetComponentInChildren<SpriteRenderer>().sprite = npcScriptableObject.NPCSprite;
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

        EventBus.Invoke(new NPCReachedTableEvent(npcScriptableObject));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventBus.Invoke(new NPCClickedEvent());
    }
}
