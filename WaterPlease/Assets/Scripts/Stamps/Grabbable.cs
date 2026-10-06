using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public enum GrabState { NotGrabbed, Grabbed}

    public GrabState grabState { get; private set; } = GrabState.NotGrabbed;

    public void OnGrab()
    {
        grabState = GrabState.Grabbed;
    }

    public void OnDrop()
    {
        grabState = GrabState.NotGrabbed;
    }
}
