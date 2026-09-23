using UnityEngine;

public class DocumentStampedEvent : IGameEvent
{
    public DocumentHandler.DocumentState DocumentState {  get; }

    public DocumentStampedEvent(DocumentHandler.DocumentState documentState)
    {
        DocumentState = documentState;
    }
}
