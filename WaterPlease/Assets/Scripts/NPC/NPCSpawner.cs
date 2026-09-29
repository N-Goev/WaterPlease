using System.Collections;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject NPCPrefab;

    private Vector3 NPCSpawnPos = new Vector3(-0.6f, 0.4f, 0f);

    public void SpawnNextNPC(NPCScriptableObject npcScriptableObject)
    {
        SpawnNPC(npcScriptableObject);
    }

    private void SpawnNPC(NPCScriptableObject npcScriptableObject)
    {
        GameObject currentNPC = Instantiate(NPCPrefab, NPCSpawnPos, Quaternion.identity);
        currentNPC.GetComponent<NPCHandler>()?.InitNPC(npcScriptableObject);
    }
}
