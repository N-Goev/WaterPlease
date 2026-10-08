using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public enum GrabState { NotGrabbed, Grabbed}

    public GrabState grabState { get; private set; } = GrabState.NotGrabbed;

    public void OnGrab()
    {
        grabState = GrabState.Grabbed;
        EventBus.Invoke(new ItemPickedUpEvent(this.gameObject));
    }

    public void OnDrag()
    {
        EventBus.Invoke(new ItemDraggedEvent(this.gameObject));
    }

    public void OnDrop()
    {
        grabState = GrabState.NotGrabbed;
        EventBus.Invoke(new ItemDroppedEvent(this.gameObject));
    }
}
