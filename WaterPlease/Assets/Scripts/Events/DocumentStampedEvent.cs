using UnityEngine;

public class DocumentStampedEvent : IGameEvent
{
    public WaterRequestHandler.DocumentState DocumentState {  get; }

    public DocumentStampedEvent(WaterRequestHandler.DocumentState documentState)
    {
        DocumentState = documentState;
    }
}
