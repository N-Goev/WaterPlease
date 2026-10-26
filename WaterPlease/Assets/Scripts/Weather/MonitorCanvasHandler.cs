using UnityEngine;

public class MonitorCanvasHandler : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        EventBus.Add<ZoomingInEvent>(OnOpenMonitor);
        EventBus.Add<ZoomingOutEvent>(OnCloseMonitor);
    }

    private void OnDisable()
    {
        EventBus.Remove<ZoomingInEvent>(OnOpenMonitor);
        EventBus.Remove<ZoomingOutEvent>(OnCloseMonitor);
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        DisableMonitorInteraction();
    }

    private void OnOpenMonitor(ZoomingInEvent zoomingInEvent)
    {
        EnableMonitorInteraction();
    }

    private void EnableMonitorInteraction()
    {
        if (canvasGroup)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    private void OnCloseMonitor(ZoomingOutEvent zoomingOutEvent)
    {
        DisableMonitorInteraction();
    }

    private void DisableMonitorInteraction()
    {
        if (canvasGroup)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
