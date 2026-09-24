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

    // Spawning NPCs
    IEnumerator SpawnNPCCoroutine(float secondsToSpawn)
    {
        yield return new WaitForSeconds(secondsToSpawn);

        SpawnNPC();
    }

    private void SpawnNPC()
    {
        if (CanSpawnNPC()) {
            Sprite NPCSprite = NPCList[0].NPCSprite;
            NPCSpawner?.SpawnNextNPC(NPCSprite);

            NPCList.RemoveAt(0);
        }
        else
        {
            EventBus.Invoke(new LevelCompleteEvent(SceneManager.GetActiveScene().buildIndex));

            TryLoadNextScene();

            print("Level Complete");
        }
    }

    private void OnDocumentStamped(DocumentStampedEvent documentStampedEvent)
    {
        StartCoroutine(SpawnNPCCoroutine(1));
        print(documentStampedEvent.DocumentState);
    }

    private bool CanSpawnNPC()
    {
        return NPCList.Count > 0;
    }

    // Spawning documents
    private void OnNPCReachedTable(NPCReachedTableEvent npcReachedTableEvent)
    {
        SpawnDocuments();
    }

    private void SpawnDocuments()
    {
        documentSpawner?.SpawnNextDocuments();
    }

    private void TryLoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
