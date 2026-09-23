using System.Collections;
using UnityEngine;

public class DocumentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject documentPrefab;
    [SerializeField] private float documentSpawnHeight;
    private Vector3 documentSpawnPos;

    private void Awake()
    {
        documentSpawnPos = new Vector3(-0.07f, documentSpawnHeight, -8.819f);
    }

    public void SpawnNextDocuments()
    {
        SpawnDocuments();
    }

    private void SpawnDocuments()
    {
        Instantiate(documentPrefab, documentSpawnPos, Quaternion.identity);
    }
}
