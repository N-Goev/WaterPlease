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

        MoveToPointerPosition();

        EventBus.Invoke(new ItemDraggedEvent(this.gameObject));
    }

    private void MoveToPointerPosition()
    {
        Vector2 pointerPos = Pointer.current.position.ReadValue();
        Vector3 position = new Vector3(pointerPos.x, pointerPos.y, Camera.main.WorldToScreenPoint(transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
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
}
