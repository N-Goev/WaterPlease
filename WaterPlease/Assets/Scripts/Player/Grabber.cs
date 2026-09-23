using UnityEngine;
using UnityEngine.InputSystem;

public class Grabber : MonoBehaviour
{
    private Rigidbody selectedObject;

    void Update()
    {
        OnDrag();
    }

    public void OnPress(InputAction.CallbackContext context)
    {
        //When pressing left click/touchscreen
        if (context.started) {
            OnGrab();
        }

        //When letting go
        if (context.canceled){
            OnDrop();
        }
    }

    private void OnGrab()
    {
        if (selectedObject != null)
        {
            return;
        }

        RaycastHit hit = CastRay();

        if (hit.collider == null)
        {
            return;
        }

        if (!hit.collider.CompareTag("DragAndDrop"))
        {
            return;
        }

        if (!hit.collider.attachedRigidbody)
        {
            return;
        }

        Rigidbody currentObject = hit.collider.attachedRigidbody;

        currentObject.useGravity = false;
        currentObject.isKinematic = true;

        selectedObject = currentObject;
        Cursor.visible = false;
    }

    private void OnDrag()
    {
        if (selectedObject == null)
        {
            return;
        }

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 position = new Vector3(mousePos.x, mousePos.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

        selectedObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, selectedObject.transform.position.z);
    }

    private void OnDrop()
    {
        if (selectedObject == null)
        {
            return;
        }

        selectedObject.useGravity = true;
        selectedObject.isKinematic = false;

        selectedObject = null;
        Cursor.visible = true;
    }

    private RaycastHit CastRay()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Vector3 screenMousePosFar = new Vector3(
                mousePos.x,
                mousePos.y,
                Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
                mousePos.x,
                mousePos.y,
                Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }
}
