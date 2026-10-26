using System.Collections.Generic;
using UnityEngine;

public class LoadLevelInfoEvent : IGameEvent
{
    public LevelScriptableObject LevelScriptableObject;

    public LoadLevelInfoEvent(LevelScriptableObject levelScriptableObject)
    {
        LevelScriptableObject = levelScriptableObject;
    }
}
