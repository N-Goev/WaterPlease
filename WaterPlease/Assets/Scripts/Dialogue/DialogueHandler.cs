using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class DialogueHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private float letterDelay;
    private string[] dialogue;

    private TextMeshProUGUI textComponent;
    private CanvasGroup canvasGroup;

    private int dialogueIndex;

    private bool CanShowDialogue = true;

    private void OnEnable()
    {
        EventBus.Add<NPCReachedTableEvent>(OnNPCReachedTable);
        EventBus.Add<DocumentStampedEvent>(OnDocumentStamped);
        EventBus.Add<NPCClickedEvent>(OnNPCClicked);
        EventBus.Add<ZoomingInEvent>(OnZoomedIn);
        EventBus.Add<ZoomingOutEvent>(OnZoomedOut);
        EventBus.Add<ItemPickedUpEvent>(OnItemPickedUp);
        EventBus.Add<ItemDraggedEvent>(OnItemDragged);
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
        EventBus.Remove<ItemDraggedEvent>(OnItemDragged);
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
        InitNewDialogue(npcReachedTableEvent.NPCScriptableObject.Dialogue);
    }

    private void InitNewDialogue(string[] newDialogue)
    {
        textComponent.text = string.Empty;
        dialogueIndex = 0;
        dialogue = newDialogue;
    }

    private void StartDialogue()
    {
        StartCoroutine(TypeLine());

        ShowDialogueBox();
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in dialogue[dialogueIndex].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    private void TypeNextLine()
    {
        if (dialogueIndex < dialogue.Length - 1)
        {
            dialogueIndex++;
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
        if (dialogue.Length <= 0 ) { return; }

        //Check if this is the first press
        if (dialogueIndex == 0 && textComponent.text == string.Empty)
        {
            StartDialogue();
            return;
        }

        //Continue to next dialogue line if current text has been fully typed out
        if (textComponent.text == dialogue[dialogueIndex])
        {
            TypeNextLine();
        }
        //Fully type out current line
        else
        {
            StopAllCoroutines();
            textComponent.text = dialogue[dialogueIndex];
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

    private void OnItemPickedUp(ItemPickedUpEvent itemPickedUpEvent)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void OnItemDragged(ItemDraggedEvent itemDraggedEvent)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void OnItemDropped(ItemDroppedEvent itemDroppedEvent)
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

}
