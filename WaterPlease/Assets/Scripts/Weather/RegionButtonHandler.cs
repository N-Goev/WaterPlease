using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RegionButtonHandler : MonoBehaviour, IPointerDownHandler
{
    public MapRegion region;

    private Image regionImage;

    private void Start()
    {
        regionImage = GetComponent<Image>();

        if(regionImage)
        {
            regionImage.alphaHitTestMinimumThreshold = 1f;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventBus.Invoke(new RegionButtonClickedEvent(region));
    }
}
