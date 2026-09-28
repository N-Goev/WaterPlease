using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class DialogueHandler : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;

    private TextMeshProUGUI textComponent;
    private CanvasGroup canvasGroup;

    private int index;

    private void OnEnable()
    {
        EventBus.Add<NPCReachedTableEvent>(StartDialogue);
        EventBus.Add<DocumentStampedEvent>(Hide);
    }

    private void OnDisable()
    {
        EventBus.Remove<NPCReachedTableEvent>(StartDialogue);
        EventBus.Remove<DocumentStampedEvent>(Hide);
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

    private void StartDialogue(NPCReachedTableEvent npcReachedTableEvent)
    {
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());

        Show();
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (textComponent.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void Hide(DocumentStampedEvent documentStampedEvent)
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
