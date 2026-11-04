using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Grabbable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    private Rigidbody rb;

    private bool isGrabbed = false;

    private float heightOffset = 0.05f;

    private void OnEnable()
    {
        EventBus.Add<DocumentStampedEvent>(OnDocumentStamped);
    }

    private void OnDisable()
    {
        EventBus.Remove<DocumentStampedEvent>(OnDocumentStamped);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //On dragged/moved
    private void Update()
    {
        if (!isGrabbed) { return; }

        MoveToPointerPosition();
    }

    //On grabbed
    public void OnPointerDown(PointerEventData eventData)
    {
        PickUpGrabbable();
    }

    private void PickUpGrabbable()
    {
        isGrabbed = true;

        DisablePhysics();

        Cursor.visible = false;

        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + heightOffset, transform.position.z);
        transform.position = newPos;

        EventBus.Invoke(new ItemPickedUpEvent(this.gameObject));
    }

    //On dropped
    public void OnPointerUp(PointerEventData eventData)
    {
        DropGrabbable();
    }

    private void DropGrabbable()
    {
        isGrabbed = false;

        EnablePhysics();

        Cursor.visible = true;

        EventBus.Invoke(new ItemDroppedEvent(this.gameObject));
    }

    //Invoke moved event
    public void OnPointerMove(PointerEventData eventData)
    {
        if(!isGrabbed) { return; }

        EventBus.Invoke(new ItemDraggedEvent(this.gameObject));
    }

    private void MoveToPointerPosition()
    {
        Vector2 pointerPos = Pointer.current.position.ReadValue();
        Vector3 position = new Vector3(pointerPos.x, pointerPos.y, Camera.main.WorldToScreenPoint(transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

        transform.position = new Vector3(worldPosition.x, worldPosition.y + heightOffset, transform.position.z);
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

    private void OnDocumentStamped(DocumentStampedEvent @event)
    {
        DropGrabbable();
    }
}
