using UnityEngine;

public class RegionButtonClickedEvent : IGameEvent
{
    public RegionButtonHandler.MapRegion Region { get; }

    public RegionButtonClickedEvent(RegionButtonHandler.MapRegion region)
    {
        Region = region;
    }
}
