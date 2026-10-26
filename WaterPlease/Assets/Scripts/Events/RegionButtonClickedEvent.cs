using UnityEngine;

public class RegionButtonClickedEvent : IGameEvent
{
    public MapRegion Region { get; }

    public RegionButtonClickedEvent(MapRegion region)
    {
        Region = region;
    }
}
