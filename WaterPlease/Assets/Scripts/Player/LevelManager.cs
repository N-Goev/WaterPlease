using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private DocumentSpawner documentSpawner;
    private NPCSpawner NPCSpawner;

    private void Awake()
    {
        documentSpawner = GetComponent<DocumentSpawner>();
        NPCSpawner = GetComponent<NPCSpawner>();
    }

    private void OnEnable()
    {
        EventBus.Add<DocumentStampedEvent>(OnDocumentStamped);
        EventBus.Add<NPCReachedTableEvent>(OnNPCReachedTable);
    }

    private void OnDisable()
    {
        EventBus.Remove<DocumentStampedEvent>(OnDocumentStamped);
        EventBus.Remove<NPCReachedTableEvent>(OnNPCReachedTable);
    }

    private void Start()
    {
        StartCoroutine(SpawnNPCCoroutine(0));
    }

    private void OnDocumentStamped(DocumentStampedEvent documentStampedEvent)
    {
        StartCoroutine(SpawnNPCCoroutine(1));
        print(documentStampedEvent.DocumentState);
    }

    private void OnNPCReachedTable(NPCReachedTableEvent npcReachedTableEvent)
    {
        SpawnDocuments();
    }

    IEnumerator SpawnNPCCoroutine(float secondsToSpawn)
    {
        yield return new WaitForSeconds(secondsToSpawn);

        SpawnNPC();
    }

    private void SpawnNPC()
    {
        NPCSpawner?.SpawnNextNPC();
    }

    private void SpawnDocuments()
    {
        documentSpawner?.SpawnNextDocuments();
    }
}
