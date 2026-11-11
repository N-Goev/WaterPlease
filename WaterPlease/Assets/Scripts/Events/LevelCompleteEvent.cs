using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteEvent : IGameEvent
{
    public List<StampResult> StampResults;
    public LevelCompleteEvent(List<StampResult> stampResults)
    {
        StampResults = stampResults;
    }
}
