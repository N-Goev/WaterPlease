using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "ScriptableObjects/NPCScriptableObject")]
public class NPCScriptableObject : ScriptableObject
{
    [SerializeField] public Sprite NPCSprite;
}
