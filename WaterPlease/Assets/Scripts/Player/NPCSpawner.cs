using System.Collections;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject NPCPrefab;

    private Vector3 NPCSpawnPos = new Vector3(-1.43f, 0f, -7.72f);

    private float finalZ = -13.17f;
    private float NPCmoveSpeed = 4f;

    public void SpawnNextNPC()
    {
        SpawnNPC();
    }

    private void SpawnNPC()
    {
        GameObject currentNPC = Instantiate(NPCPrefab, NPCSpawnPos, Quaternion.identity);

        StartCoroutine(MoveNPCCoroutine(currentNPC));
    }

    IEnumerator MoveNPCCoroutine(GameObject currentNPC)
    {
        while (currentNPC.transform.position.z > finalZ)
        {
            Vector3 currentNPCPos = currentNPC.transform.position;
            currentNPC.transform.position = new Vector3(currentNPCPos.x, currentNPCPos.y, currentNPCPos.z - (NPCmoveSpeed * Time.deltaTime));

            yield return null;
        }

        EventBus.Invoke(new NPCReachedTableEvent());
    }
}
