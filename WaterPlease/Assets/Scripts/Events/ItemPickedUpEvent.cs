using UnityEngine;

public class ItemPickedUpEvent : IGameEvent
{
    public GameObject Item;

    public ItemPickedUpEvent(GameObject item)
    {
        Item = item;
    }
}
