using UnityEngine;

public class WaterRequestHandler : MonoBehaviour
{
    public enum DocumentState { None, Approved, Denied}

    private bool graded = false;
    private DocumentState documentState = DocumentState.None;

    private void OnTriggerEnter(Collider other)
    {
        if (graded)
        {
            return;
        }

        Grabbable currentGrabbable = other.gameObject.GetComponent<Grabbable>();

        if (!currentGrabbable || currentGrabbable.grabState == Grabbable.GrabState.NotGrabbed)
        {
            return;
        }

        StampHandler currentStamp = other.gameObject.GetComponent<StampHandler>();

        if (!currentStamp)
        {
            return;
        }

        switch (currentStamp.stampType)
        {
            case StampHandler.StampType.Approve:
                documentState = DocumentState.Approved;
                break;
            case StampHandler.StampType.Deny:
                documentState = DocumentState.Denied;
                break;
            default:
                break;
        }

        graded = true;
        EventBus.Invoke(new DocumentStampedEvent(documentState));
        Destroy(gameObject);
    }
}
