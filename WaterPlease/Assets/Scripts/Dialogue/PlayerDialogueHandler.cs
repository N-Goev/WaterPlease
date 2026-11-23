using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerDialogueHandler : MonoBehaviour
{
    [SerializeField] private float letterDelay;
    private string dialogue;

    private TextMeshProUGUI textComponent;
    private CanvasGroup canvasGroup;

    private bool CanShowDialogue = true;

    private void OnEnable()
    {
        EventBus.Add<PlayerSpeaksEvent>(OnPlayerSpeaks);
        EventBus.Add<PlayerStopSpeakingEvent>(OnPlayerStopSpeaking);
        //EventBus.Add<ZoomingInEvent>(OnZoomedIn);
        //EventBus.Add<ZoomingOutEvent>(OnZoomedOut);
    }

    private void OnDisable()
    {
        EventBus.Remove<PlayerSpeaksEvent>(OnPlayerSpeaks);
        EventBus.Remove<PlayerStopSpeakingEvent>(OnPlayerStopSpeaking);
        //EventBus.Remove<ZoomingInEvent>(OnZoomedIn);
        //EventBus.Remove<ZoomingOutEvent>(OnZoomedOut);
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

    private void InitNewDialogue(string newDialogue)
    {
        textComponent.text = string.Empty;
        dialogue = newDialogue;
    }

    private void StartDialogue()
    {
        StartCoroutine(TypeLine());

        ShowDialogueBox();
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in dialogue.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    private void OnPlayerSpeaks(PlayerSpeaksEvent playerSpeaksEvent)
    {
        InitNewDialogue(playerSpeaksEvent.PlayerDialogue);
        StartDialogue();
    }

    private void OnPlayerStopSpeaking(PlayerStopSpeakingEvent playerStopSpeakingEvent)
    {
        StopDialogue();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnInteract();
    }

    private void OnInteract()
    {
        if (!textComponent) { return; }
        if (dialogue == null) { return; }
        if (dialogue.Length <= 0) { return; }

        //Continue to next dialogue line if current text has been fully typed out
        if (textComponent.text != dialogue)
        {
            StopAllCoroutines();
            textComponent.text = dialogue;
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

    private void StopDialogue()
    {
        StopAllCoroutines();
        HideDialogueBox();
    }

    private void OnZoomedIn(ZoomingInEvent zoomingInEvent)
    {
        CanShowDialogue = false;
        HideDialogueBox();
    }

    private void OnZoomedOut(ZoomingOutEvent zoomingOutEvent)
    {
        CanShowDialogue = true;
        ShowDialogueBox();
    }
}
