using System.Collections;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject NPCPrefab;

    private Vector3 NPCSpawnPos = new Vector3(-1.43f, 0f, -7.72f);

    public void SpawnNextNPC(Sprite NPCSprite)
    {
        SpawnNPC(NPCSprite);
    }

    private void SpawnNPC(Sprite NPCSprite)
    {
        GameObject currentNPC = Instantiate(NPCPrefab, NPCSpawnPos, Quaternion.identity);
        if (currentNPC.GetComponentInChildren<SpriteRenderer>())
        {
            currentNPC.GetComponentInChildren<SpriteRenderer>().sprite = NPCSprite;
        }
    }
}
