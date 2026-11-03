using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Grabbable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    private Rigidbody rb;

    private bool isGrabbed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //On grabbed
    public void OnPointerDown(PointerEventData eventData)
    {
        isGrabbed = true;

        DisablePhysics();

        Cursor.visible = false;

        EventBus.Invoke(new ItemPickedUpEvent(this.gameObject));
    }

    //On dropped
    public void OnPointerUp(PointerEventData eventData)
    {
        isGrabbed = false;

        EnablePhysics();

        Cursor.visible = true;

        EventBus.Invoke(new ItemDroppedEvent(this.gameObject));
    }

    //On dragged
    public void OnPointerMove(PointerEventData eventData)
    {
        if(!isGrabbed) { return; }

        MoveToMousePosition();

        EventBus.Invoke(new ItemDraggedEvent(this.gameObject));
    }

    private void EnablePhysics()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    private void DisablePhysics()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void MoveToMousePosition()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 position = new Vector3(mousePos.x, mousePos.y, Camera.main.WorldToScreenPoint(transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }
}
