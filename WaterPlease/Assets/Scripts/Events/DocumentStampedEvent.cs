using UnityEngine;

public class DocumentStampedEvent : IGameEvent
{
    public StampResult stampResult {  get; }

    public DocumentStampedEvent(StampResult stampResult)
    {
        this.stampResult = stampResult;
    }
}
