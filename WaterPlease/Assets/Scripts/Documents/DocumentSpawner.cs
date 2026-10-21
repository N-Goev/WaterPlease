using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DocumentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject UIListElementPrefab;

    [Header("Water Credentials")]
    [SerializeField] private GameObject WaterCredentialsPrefab;

    [Header("Water Request")]
    [SerializeField] private GameObject waterRequestDocumentPrefab;

    [Header("Spawn Properties")]
    [SerializeField] private Vector3 document1SpawnPos;
    [SerializeField] private Vector3 document2SpawnPos;

    public void SpawnNextDocuments(NPCReachedTableEvent npcReachedTableEvent)
    {
        SpawnDocuments(npcReachedTableEvent.NPCScriptableObject);
    }

    private void SpawnDocuments(NPCScriptableObject npcScriptableObject)
    {
        GameObject waterCredentialsInstance = Instantiate(WaterCredentialsPrefab, document1SpawnPos, Quaternion.identity);
        GameObject waterRequestInstance = Instantiate(waterRequestDocumentPrefab, document2SpawnPos, Quaternion.identity);

        UpdateWaterCredentialsText(waterCredentialsInstance, npcScriptableObject);
        UpdateWaterRequestText(waterRequestInstance, npcScriptableObject);
    }

    private void UpdateWaterRequestText(GameObject waterRequestInstance, NPCScriptableObject npcScriptableObject)
    {
        TextMeshProUGUI[] TextElements = waterRequestInstance.GetComponentsInChildren<TextMeshProUGUI>();
        if (TextElements.Length == 5)
        {
            TextElements[0].text = npcScriptableObject.WaterRequest;
            TextElements[1].text = npcScriptableObject.Name + " " + npcScriptableObject.Surname;
            TextElements[2].text = npcScriptableObject.Residency;
            TextElements[3].text = npcScriptableObject.DateAndPlace;
            TextElements[4].text = npcScriptableObject.Sign;
        }

        VerticalLayoutGroup[] Lists = waterRequestInstance.GetComponentsInChildren<VerticalLayoutGroup>();

        if (Lists.Length < 2)
        {
            Debug.LogError("Missing Vertical Layout Group in document prefab");
            return;
        }

        List<string> reasonsForWaterRequest = npcScriptableObject.ReasonsForWaterRequest;
        foreach (string reason in reasonsForWaterRequest) {
            GameObject reasonInstance = Instantiate(UIListElementPrefab, Lists[0].transform);
            TextMeshProUGUI reasonText = reasonInstance.GetComponent<TextMeshProUGUI>();

            if (reasonText)
            {
                reasonText.text = reason;
            }
        }

        Dictionary<string, int> TopUsesOfWater = npcScriptableObject.TopUsesOfWater;
        foreach(KeyValuePair<string, int> currentPair in TopUsesOfWater) 
        {
            GameObject waterUseInstance = Instantiate(UIListElementPrefab, Lists[1].transform);
            TextMeshProUGUI waterUseText = waterUseInstance.GetComponent<TextMeshProUGUI>();

            if (waterUseText) {
                waterUseText.text = currentPair.Key + " - " + currentPair.Value + "L";
            }
        }
    }

    private void UpdateWaterCredentialsText(GameObject waterCredentialsInstance, NPCScriptableObject npcScriptableObject)
    {
        TextMeshProUGUI[] TextElements = waterCredentialsInstance.GetComponentsInChildren<TextMeshProUGUI>();

        if(TextElements.Length == 7)
        {
            TextElements[0].text = npcScriptableObject.Surname;
            TextElements[1].text = npcScriptableObject.Name;
            TextElements[2].text = npcScriptableObject.Sex.ToString();
            TextElements[3].text = npcScriptableObject.Nationality;
            TextElements[4].text = npcScriptableObject.DateOfBirth;
            TextElements[5].text = npcScriptableObject.DateOfIssue;
            TextElements[6].text = npcScriptableObject.DateOfExpiry;
        }
    }
}
