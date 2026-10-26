using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum MapRegion { CenterNedschee, NorthNedschee, SouthNedschee, EastNedschee, WestNedschee, CenterLogehen, SouthLogehen, EastLogehen }

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
        print("Pressed " + region.ToString());
        EventBus.Invoke(new RegionButtonClickedEvent(region));
    }
}
