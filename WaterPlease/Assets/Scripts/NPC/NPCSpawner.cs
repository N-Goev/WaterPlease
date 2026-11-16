using System.Collections;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject NPCPrefab;

    [SerializeField] private Vector3 NPCSpawnPos = new Vector3(-0.6f, 1.5f, -3.5f);
    [SerializeField] private Vector3 NPCDeskPos = new Vector3(-1f, 1.5f, -3.5f);
    [SerializeField] private Vector3 NPCFinalPos = new Vector3(-0.6f, 1.5f, -3.5f);

    public void SpawnNextNPC(NPCScriptableObject npcScriptableObject)
    {
        SpawnNPC(npcScriptableObject);
    }

    private void SpawnNPC(NPCScriptableObject npcScriptableObject)
    {
        GameObject currentNPC = Instantiate(NPCPrefab, NPCSpawnPos, Quaternion.identity);
        currentNPC.GetComponent<NPCHandler>()?.InitNPC(npcScriptableObject, NPCDeskPos, NPCFinalPos);
    }
}
