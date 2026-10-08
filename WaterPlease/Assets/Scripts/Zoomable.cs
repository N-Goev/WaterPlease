using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zoomable : MonoBehaviour, IPointerDownHandler
{
    private float distanceFromObject = 0.4f;
    private float zoomInTime = 0.4f;
    private float zoomOutTime = 0.3f;

    private enum ZoomState {NotZoomed, Zooming, Zoomed, ZoomingOut}
    private ZoomState zoomState = ZoomState.NotZoomed;

    //Components
    private UnityEngine.UI.Button stopInspectingButton;

    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;

    void Awake()
    {
        stopInspectingButton = GetComponentInChildren<UnityEngine.UI.Button>();

        originalCameraPosition = Camera.main.transform.position;
        originalCameraRotation = Camera.main.transform.rotation;
    }

    void Start()
    {
        stopInspectingButton.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ZoomIn();
    }

    private void ZoomIn()
    {
        if (zoomState == ZoomState.NotZoomed) {
            StartCoroutine(ZoomInCoroutine(zoomInTime));
            EventBus.Invoke(new ZoomingInEvent());
        }
    }

    IEnumerator ZoomInCoroutine(float time)
    {
        ChangeState(ZoomState.Zooming);

        Vector3 startingPos = originalCameraPosition;
        Vector3 finalPos = gameObject.transform.position - (gameObject.transform.forward * distanceFromObject);

        Quaternion startingRotation = originalCameraRotation;
        Vector3 lookDirection = gameObject.transform.position - finalPos;
        Quaternion finalRotation = Quaternion.LookRotation(lookDirection);

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            Camera.main.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            Camera.main.transform.rotation = Quaternion.Lerp(startingRotation, finalRotation, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ChangeState(ZoomState.Zoomed);
    }

    public void ZoomOut()
    {
        if(zoomState == ZoomState.Zoomed)
        {
            StartCoroutine(ZoomOutCoroutine(zoomOutTime));
        }
    }

    IEnumerator ZoomOutCoroutine(float time)
    {
        ChangeState(ZoomState.ZoomingOut);

        Vector3 startingPos = Camera.main.transform.position;
        Vector3 finalPos = originalCameraPosition;

        Quaternion startingRotation = Camera.main.transform.rotation;
        Quaternion finalRotation = originalCameraRotation;

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            Camera.main.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            Camera.main.transform.rotation = Quaternion.Lerp(startingRotation, finalRotation, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ChangeState(ZoomState.NotZoomed);
        EventBus.Invoke(new ZoomingOutEvent());
    }

    private void ChangeState(ZoomState state)
    {
        zoomState = state;
        OnStateChange();
    }

    private void OnStateChange()
    {

        switch (zoomState)
        {
            case ZoomState.NotZoomed:
                stopInspectingButton.gameObject.SetActive(false);
                break;

            case ZoomState.Zooming:
                stopInspectingButton.gameObject.SetActive(false);
                break;
            case ZoomState.Zoomed:
                stopInspectingButton.gameObject.SetActive(true);
                break;
            case ZoomState.ZoomingOut:
                stopInspectingButton.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
