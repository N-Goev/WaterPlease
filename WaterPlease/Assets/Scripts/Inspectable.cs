using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inspectable : MonoBehaviour, IPointerDownHandler
{
    private float distanceFromCamera = 0.5f;

    private enum InspectingState {NotInspecting, Inspecting}
    private InspectingState inspectingState = InspectingState.NotInspecting;

    //Components
    private Rigidbody rigidbody;
    private UnityEngine.UI.Button stopInspectingButton;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        stopInspectingButton = GetComponentInChildren<UnityEngine.UI.Button>();

        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Start()
    {
        stopInspectingButton.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Inspect();
    }

    private void ChangeState(InspectingState state)
    {
        inspectingState = state;
        OnStateChange();
    }

    private void OnStateChange() {
        if (!rigidbody){ return; }

        switch (inspectingState)
        {
            case InspectingState.NotInspecting:
                rigidbody.isKinematic = false;
                stopInspectingButton.gameObject.SetActive(false);
                break;

            case InspectingState.Inspecting:
                rigidbody.isKinematic = true;
                stopInspectingButton.gameObject.SetActive(true);
                break;

            default:
                break;
        }
    }

    private void Inspect()
    {
        //Place item in front of the player
        gameObject.transform.position = Camera.main.transform.position + (Camera.main.transform.forward * distanceFromCamera);

        //rotate to face the camera
        Vector3 lookDirection = gameObject.transform.position - Camera.main.transform.position;
        gameObject.transform.rotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(-90, 0, 0);

        ChangeState(InspectingState.Inspecting);
    }

    public void PutDown()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        ChangeState(InspectingState.NotInspecting);
    }
}
