using UnityEngine;

public class RegionButtonsGroupHandler : MonoBehaviour
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
    }

    private void OnOpenMonitor(ZoomingInEvent zoomingInEvent)
    {
        if (canvasGroup)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    private void OnCloseMonitor(ZoomingOutEvent zoomingOutEvent)
    {
        if (canvasGroup)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
