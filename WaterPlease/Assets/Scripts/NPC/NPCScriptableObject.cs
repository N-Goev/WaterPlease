using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "ScriptableObjects/NPCScriptableObject")]
public class NPCScriptableObject : ScriptableObject
{
    public enum SexOption { M, F, OTH}

    public Sprite NPCSprite;

    public string[] Dialogue;

    [Header("ID")]
    public string Name;
    public string Surname;
    public SexOption Sex;
    public string Nationality;
    public string DateOfBirth;
    public string DateOfIssue;
    public string DateOfExpiry;

    [Header("Water Request")]

    public string WaterRequest;
    public List<string> ReasonsForWaterRequest;
    public string Residency;

    [SerializeField]
    [DictionaryDisplay(keyLabel = "Use", valueLabel = "Litres of water")]
    public Dictionary<string, int> TopUsesOfWater;

    public string DateAndPlace;
    public string Sign;
}
