using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "ScriptableObjects/NPCScriptableObject")]
public class NPCScriptableObject : ScriptableObject
{
    public Sprite NPCSprite;

    public string[] Dialogue;

    public string Name;
    public string Age;
    public string Request;

    [SerializeField]
    [DictionaryDisplay(keyLabel = "Use", valueLabel = "Litres of water")]
    public Dictionary<string, int> TopUsesOfWater;
}
