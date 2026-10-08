using System.Collections.Generic;
using UnityEngine;

public class WaterRequestHandler : MonoBehaviour
{
    public enum DocumentState { None, Approved, Denied}

    [SerializeField] Material outlineMaterial;
    MeshRenderer meshRenderer;

    private bool graded = false;
    private DocumentState documentState = DocumentState.None;

    private void OnEnable()
    {
        EventBus.Add<ItemPickedUpEvent>(OnStampPickedUp);
        EventBus.Add<ItemDraggedEvent>(OnStampDragged);
        EventBus.Add<ItemDroppedEvent>(OnStampDropped);
    }

    private void OnDisable() {
        EventBus.Remove<ItemPickedUpEvent>(OnStampPickedUp);
        EventBus.Remove<ItemDraggedEvent>(OnStampDragged);
        EventBus.Remove<ItemDroppedEvent>(OnStampDropped);
    }

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (graded)
        {
            return;
        }

        Grabbable currentGrabbable = other.gameObject.GetComponent<Grabbable>();

        if (!currentGrabbable || currentGrabbable.grabState == Grabbable.GrabState.NotGrabbed)
        {
            return;
        }

        StampHandler currentStamp = other.gameObject.GetComponent<StampHandler>();

        if (!currentStamp)
        {
            return;
        }

        switch (currentStamp.stampType)
        {
            case StampHandler.StampType.Approve:
                documentState = DocumentState.Approved;
                break;
            case StampHandler.StampType.Deny:
                documentState = DocumentState.Denied;
                break;
            default:
                break;
        }

        graded = true;
        EventBus.Invoke(new DocumentStampedEvent(documentState));
        Destroy(gameObject);
    }

    private void OnStampPickedUp(ItemPickedUpEvent itemPickedUpEvent)
    {
        if (!itemPickedUpEvent.Item.GetComponent<StampHandler>()){return;}

        if (!meshRenderer) {  return; }

        if(meshRenderer.materials.Length > 1){ return; }

        List<Material> newMaterials = new List<Material>{ meshRenderer.materials[0], outlineMaterial };

        meshRenderer.SetMaterials(newMaterials);
    }

    private void OnStampDragged(ItemDraggedEvent itemDraggedEvent)
    {
        if (!itemDraggedEvent.Item.GetComponent<StampHandler>()) { return; }

        if (!meshRenderer) { return; }

        if (meshRenderer.materials.Length > 1) { return; }

        List<Material> newMaterials = new List<Material> { meshRenderer.materials[0], outlineMaterial };

        meshRenderer.SetMaterials(newMaterials);
    }

    private void OnStampDropped(ItemDroppedEvent itemDroppedEvent)
    {
        if (!itemDroppedEvent.Item.GetComponent<StampHandler>()) { return; }

        if (!meshRenderer) { return; }

        if (meshRenderer.materials.Length <= 1) { return; }

        List<Material> newMaterials = new List<Material> { meshRenderer.materials[0]};

        meshRenderer.SetMaterials(newMaterials);
    }
}
