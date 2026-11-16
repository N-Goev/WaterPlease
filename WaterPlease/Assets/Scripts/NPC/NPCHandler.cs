using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCHandler : MonoBehaviour, IPointerDownHandler
{
    private Vector3 deskPos; 
    private Vector3 finalPos; 
    private float moveSpeed = 1f;

    NPCScriptableObject npcScriptableObject;

    private void OnEnable()
    {
        EventBus.Add<DocumentStampedEvent>(OnDocumentStamped);
    }

    private void OnDisable()
    {
        EventBus.Remove<DocumentStampedEvent>(OnDocumentStamped);
    }

    public void InitNPC(NPCScriptableObject npcScriptableObject)
    {
        this.npcScriptableObject = npcScriptableObject;
        GetComponentInChildren<SpriteRenderer>().sprite = npcScriptableObject.NPCSprite;

        StartCoroutine(MoveNPCToDeskCoroutine());
    }

    public void InitNPC(NPCScriptableObject npcScriptableObject, Vector3 deskPosition, Vector3 finalPosition)
    {
        this.npcScriptableObject = npcScriptableObject;
        GetComponentInChildren<SpriteRenderer>().sprite = npcScriptableObject.NPCSprite;

        deskPos = deskPosition;
        finalPos = finalPosition;

        StartCoroutine(MoveNPCToDeskCoroutine());
    }

    private void OnDocumentStamped(DocumentStampedEvent documentStampedEvent)
    {
        StartCoroutine(MoveNPCAwayCoroutine());
    }

    IEnumerator MoveNPCToDeskCoroutine()
    {
        float fraction = 0;

        Vector3 startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        while (fraction < 1)
        {
            fraction += Time.deltaTime * moveSpeed;

            transform.position = Vector3.Lerp(startPos, deskPos, fraction);

            yield return null;
        }

        EventBus.Invoke(new NPCReachedTableEvent(npcScriptableObject));
    }

    IEnumerator MoveNPCAwayCoroutine()
    {
        float fraction = 0;

        Vector3 startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        while (fraction < 1)
        {
            fraction += Time.deltaTime * moveSpeed;

            transform.position = Vector3.Lerp(startPos, finalPos, fraction);

            yield return null;
        }

        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventBus.Invoke(new NPCClickedEvent());
    }
}
