using System.Collections;
using TMPro;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] float transitionTime;

    private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        EventBus.Add<LoadLevelInfoEvent>(OnSceneLoad);
    }

    private void OnDisable()
    {
        EventBus.Remove<LoadLevelInfoEvent>(OnSceneLoad);
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnSceneLoad(LoadLevelInfoEvent loadLevelInfoEvent)
    {
        StartCoroutine(BeginTransitionCoroutine());
    }

    private IEnumerator BeginTransitionCoroutine()
    {
        if (!canvasGroup) { yield return null; }

        yield return new WaitForSeconds(transitionTime);

        GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);

        while (canvasGroup.alpha > 0) {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        EventBus.Invoke(new FadeAnimationCompleteEvent());

        yield return null;
    }
}
