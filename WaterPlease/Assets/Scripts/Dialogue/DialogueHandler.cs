using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class DialogueHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private float letterDelay;
    private string[] dialogue;

    private TextMeshProUGUI textComponent;
    private CanvasGroup canvasGroup;

    private int index;

    private void OnEnable()
    {
        EventBus.Add<NPCReachedTableEvent>(OnNPCReachedTable);
        EventBus.Add<DocumentStampedEvent>(OnDocumentStamped);
        EventBus.Add<NPCClickedEvent>(OnNPCClicked);
    }

    private void OnDisable()
    {
        EventBus.Remove<NPCReachedTableEvent>(OnNPCReachedTable);
        EventBus.Remove<DocumentStampedEvent>(OnDocumentStamped);
        EventBus.Remove<NPCClickedEvent>(OnNPCClicked);
    }

    void Awake()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        Hide();
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

        Show();
    }

    IEnumerator TypeLine()
    {
        foreach (char c in dialogue[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnInteract();
    }

    private void OnNPCClicked(NPCClickedEvent npcClickedEvent)
    {
        OnInteract();
    }

    private void OnInteract()
    {
        if (textComponent.text == dialogue[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = dialogue[index];
        }
    }

    private void NextLine()
    {
        if(index < dialogue.Length - 1)
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

    

    private void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void OnDocumentStamped(DocumentStampedEvent documentStampedEvent)
    {
        Hide();
    }

    private void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
