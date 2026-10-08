using UnityEngine;

public class ItemDraggedEvent : IGameEvent
{
    public GameObject Item;

    public ItemDraggedEvent(GameObject item)
    {
        Item = item;
    }
}
