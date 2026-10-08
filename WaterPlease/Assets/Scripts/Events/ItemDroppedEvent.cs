using UnityEngine;

public class ItemDroppedEvent : IGameEvent
{
    public GameObject Item;

    public ItemDroppedEvent(GameObject item)
    {
        Item = item;
    }
}
