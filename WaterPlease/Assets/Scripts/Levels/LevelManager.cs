using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelScriptableObject Level;

    private NPCSpawner NPCSpawner;
    private DocumentSpawner documentSpawner;

    private List<NPCScriptableObject> NPCList;

    private void Awake()
    {
        documentSpawner = GetComponent<DocumentSpawner>();
        NPCSpawner = GetComponent<NPCSpawner>();

        NPCList = new List<NPCScriptableObject>(Level.NPCList);
    }

    private void OnEnable()
    {
        EventBus.Add<DocumentStampedEvent>(OnDocumentStamped);
        EventBus.Add<NPCReachedTableEvent>(OnNPCReachedTable);
        EventBus.Add<FadeAnimationCompleteEvent>(OnLevelStart);
    }

    private void OnDisable()
    {
        EventBus.Remove<DocumentStampedEvent>(OnDocumentStamped);
        EventBus.Remove<NPCReachedTableEvent>(OnNPCReachedTable);
        EventBus.Remove<FadeAnimationCompleteEvent>(OnLevelStart);
    }

    private void Start()
    {
        EventBus.Invoke(new LoadLevelInfoEvent(Level));
    }

    private void OnLevelStart(FadeAnimationCompleteEvent fadeAnimationCompleteEvent)
    {
        StartCoroutine(SpawnNPCCoroutine(0));

        Cursor.visible = true;
    }

    // Spawning NPCs
    IEnumerator SpawnNPCCoroutine(float secondsToSpawn)
    {
        if (CanSpawnNPC())
        {
            yield return new WaitForSeconds(secondsToSpawn);

            SpawnNPC();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            EventBus.Invoke(new LevelCompleteEvent());

            Cursor.visible = true;
        }
    }

    private void SpawnNPC()
    {
        NPCSpawner?.SpawnNextNPC(NPCList[0]);

        NPCList.RemoveAt(0);
    }

    private void OnDocumentStamped(DocumentStampedEvent documentStampedEvent)
    {
        StartCoroutine(SpawnNPCCoroutine(1));
    }

    private bool CanSpawnNPC()
    {
        return NPCList.Count > 0;
    }

    // Spawning documents
    private void OnNPCReachedTable(NPCReachedTableEvent npcReachedTableEvent)
    {
        SpawnDocuments(npcReachedTableEvent);
    }

    private void SpawnDocuments(NPCReachedTableEvent npcReachedTableEvent)
    {
        documentSpawner?.SpawnNextDocuments(npcReachedTableEvent);
    }
}
