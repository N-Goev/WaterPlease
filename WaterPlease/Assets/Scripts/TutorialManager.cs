using System;
using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private bool HasFirstNPCBeenClicked = false;

    private void OnEnable()
    {
        EventBus.Add<NPCReachedTableEvent>(OnNPCReachedTable);
        EventBus.Add<NPCClickedEvent>(OnNPCClicked);
    }

    private void OnDisable()
    {
        EventBus.Remove<NPCReachedTableEvent>(OnNPCReachedTable);
        EventBus.Remove<NPCClickedEvent>(OnNPCClicked);
    }

    private void TutorialDialogue(string tutorialDialogue)
    {
        EventBus.Invoke(new PlayerSpeaksEvent(tutorialDialogue));
    }

    private IEnumerator TutorialDialogueWithDelay(string tutorialDialogue, float delay)
    {
        yield return new WaitForSeconds(delay);
        TutorialDialogue(tutorialDialogue);
    }

    private void OnNPCReachedTable(NPCReachedTableEvent nPCReachedTableEvent)
    {
        if(nPCReachedTableEvent.NPCScriptableObject.Name == "Aref"){
            string tutorialDialogue = "I should start by talking to the person \r\n[<i>click on the character to talk with him</i>]";
            TutorialDialogue(tutorialDialogue);
        }
    }

    private void OnNPCClicked(NPCClickedEvent nPCClickedEvent)
    {
        if (HasFirstNPCBeenClicked) { return; }

        HasFirstNPCBeenClicked = true;
        EventBus.Invoke(new PlayerStopSpeakingEvent());

        string playerDialogue = "I should check the water credentials to find more information about the person. ";
        StartCoroutine(TutorialDialogueWithDelay(playerDialogue, 4f));
    }
}
