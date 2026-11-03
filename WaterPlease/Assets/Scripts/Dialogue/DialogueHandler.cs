using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

public class DialogueHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private float letterDelay;
    private string[] dialogue;

    private TextMeshProUGUI textComponent;
    private CanvasGroup canvasGroup;

    private int index;

    private bool CanShowDialogue = true;

    private void OnEnable()
    {
        EventBus.Add<NPCReachedTableEvent>(OnNPCReachedTable);
        EventBus.Add<DocumentStampedEvent>(OnDocumentStamped);
        EventBus.Add<NPCClickedEvent>(OnNPCClicked);
        EventBus.Add<ZoomingInEvent>(OnZoomedIn);
        EventBus.Add<ZoomingOutEvent>(OnZoomedOut);
        EventBus.Add<ItemPickedUpEvent>(OnItemPickedUp);
        EventBus.Add<ItemDroppedEvent>(OnItemDropped);
    }

    private void OnDisable()
    {
        EventBus.Remove<NPCReachedTableEvent>(OnNPCReachedTable);
        EventBus.Remove<DocumentStampedEvent>(OnDocumentStamped);
        EventBus.Remove<NPCClickedEvent>(OnNPCClicked);
        EventBus.Remove<ZoomingInEvent>(OnZoomedIn);
        EventBus.Remove<ZoomingOutEvent>(OnZoomedOut);
        EventBus.Remove<ItemPickedUpEvent>(OnItemPickedUp);
        EventBus.Remove<ItemDroppedEvent>(OnItemDropped);
    }

    private void Awake()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        HideDialogueBox();
    }

    private void OnNPCReachedTable(NPCReachedTableEvent npcReachedTableEvent)
    {
        StartDialogue(npcReachedTableEvent.NPCScriptableObject.Dialogue);
    }

    private void StartDialogue(string[] newDialogue)
    {
        textComponent.text = string.Empty;
        index = 0;
        dialogue = newDialogue;
        StartCoroutine(TypeLine());

        ShowDialogueBox();
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in dialogue[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    private void GoToNextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            //Hide();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnInteract();
    }

    private void OnInteract()
    {
        if (!textComponent) { return; }
        if (dialogue == null) { return; }
        if (dialogue.Length <=0 ) { return; }

        if (textComponent.text == dialogue[index])
        {
            GoToNextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = dialogue[index];
        }
    }

    private void ShowDialogueBox()
    {
        if (CanShowDialogue)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        } 
    }

    private void HideDialogueBox()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void OnNPCClicked(NPCClickedEvent npcClickedEvent)
    {
        OnInteract();
    }

    private void OnDocumentStamped(DocumentStampedEvent documentStampedEvent)
    {
        StopAllCoroutines();
        HideDialogueBox();
    }

    private void OnZoomedIn(ZoomingInEvent zoomingInEvent) {
        CanShowDialogue = false;
        HideDialogueBox();
    }

    private void OnZoomedOut(ZoomingOutEvent zoomingOutEvent)
    {
        CanShowDialogue = true;
        ShowDialogueBox();
    }

    private void OnItemDropped(ItemDroppedEvent @event)
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void OnItemPickedUp(ItemPickedUpEvent @event)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
