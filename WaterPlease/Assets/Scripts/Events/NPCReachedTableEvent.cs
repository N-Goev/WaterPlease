using UnityEngine;

public class NPCReachedTableEvent : IGameEvent
{
    public NPCScriptableObject NPCScriptableObject;

    public NPCReachedTableEvent(NPCScriptableObject npcScriptableObject) {
        NPCScriptableObject = npcScriptableObject;
    }
}
