using UnityEngine;

public class LevelCompleteEvent : IGameEvent
{
    public int SceneIndex;

    public LevelCompleteEvent(int sceneIndex)
    {
        SceneIndex = sceneIndex;
    }
}
