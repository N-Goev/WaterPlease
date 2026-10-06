using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DocumentSpawner : MonoBehaviour
{
    [Header("Water Credentials")]
    [SerializeField] private GameObject WaterCredentialsPrefab;
    [SerializeField] private GameObject waterUseElementPrefab;

    [Header("Water Request")]
    [SerializeField] private GameObject waterRequestDocumentPrefab;

    [Header("Spawn Properties")]
    [SerializeField] private float spawnHeight;
    private Vector3 documentSpawnPos;
    private Vector3 waterRequestDocumentSpawnPos;

    private void Awake()
    {
        documentSpawnPos = new Vector3(-0.3f, spawnHeight, -8.8f);
        waterRequestDocumentSpawnPos = new Vector3(0f, spawnHeight, -8.8f);
    }

    public void SpawnNextDocuments(NPCReachedTableEvent npcReachedTableEvent)
    {
        SpawnDocuments(npcReachedTableEvent.NPCScriptableObject);
    }

    private void SpawnDocuments(NPCScriptableObject npcScriptableObject)
    {
        GameObject waterCredentialsInstance = Instantiate(WaterCredentialsPrefab, documentSpawnPos, Quaternion.identity);
        GameObject waterRequestInstance = Instantiate(waterRequestDocumentPrefab, waterRequestDocumentSpawnPos, Quaternion.identity);

        UpdateWaterCredentialsText(waterCredentialsInstance, npcScriptableObject);
        UpdateWaterRequestText(waterRequestInstance, npcScriptableObject);
    }

    private void UpdateWaterCredentialsText(GameObject waterCredentialsInstance, NPCScriptableObject npcScriptableObject)
    {
        VerticalLayoutGroup ListOfWaterUsesCanvas = waterCredentialsInstance.GetComponentInChildren<VerticalLayoutGroup>();

        if (!ListOfWaterUsesCanvas)
        {
            Debug.LogError("Missing Vertical Layout Group in document prefab");
            return;
        }

        Dictionary<string, int> TopUsesOfWater = npcScriptableObject.TopUsesOfWater;
        int totalWaterUsed = 0;

        foreach(KeyValuePair<string, int> currentPair in TopUsesOfWater) 
        {
            GameObject waterUseInstance = Instantiate(waterUseElementPrefab, ListOfWaterUsesCanvas.transform);
            TextMeshProUGUI waterUseText = waterUseInstance.GetComponent<TextMeshProUGUI>();

            if (waterUseText) {
                waterUseText.text = currentPair.Key + " - " + currentPair.Value + "L";
                totalWaterUsed += currentPair.Value;
            }
        }

        TextMeshProUGUI[] TextElements = waterCredentialsInstance.GetComponentsInChildren<TextMeshProUGUI>();
        if (TextElements.Length > 1) {
            TextElements[1].text = "Used: " + totalWaterUsed + "L"; 
        }
    }

    private void UpdateWaterRequestText(GameObject waterRequestInstance, NPCScriptableObject npcScriptableObject)
    {
        TextMeshProUGUI[] TextElements = waterRequestInstance.GetComponentsInChildren<TextMeshProUGUI>();

        if(TextElements.Length == 3)
        {
            TextElements[0].text = "Name: " + npcScriptableObject.Name;
            TextElements[1].text = "Age: " + npcScriptableObject.Age;
            TextElements[2].text = "Request: " + npcScriptableObject.Request;
        }
    }
}
