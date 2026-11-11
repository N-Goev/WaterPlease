using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewsManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject commentPrefab;

    [SerializeField] private GameObject previousPageButton;
    [SerializeField] private GameObject nextPageButton;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private List<GameObject> pages;

    private NewsOutcomeHandler outcomeHandler;

    private int currentPageIndex = 0;

    private void OnEnable()
    {
        EventBus.Add<LevelCompleteEvent>(OnLevelComplete);
    }

    private void OnDisable()
    {
        EventBus.Remove<LevelCompleteEvent>(OnLevelComplete);
    }

    private void Awake()
    {
        outcomeHandler = GetComponent<NewsOutcomeHandler>();

        DisableAll();
    }

    private void ParseStampResult(List<StampResult> stampResults)
    {
        if (!outcomeHandler) { return; }

        NewsOutcomeScriptableObject outcomeInfo = outcomeHandler.GetOutcomeInfo(SceneManager.GetActiveScene().buildIndex, stampResults);

        if (outcomeInfo == null) { return; }

        Image[] pageImages = pages[0].GetComponentsInChildren<Image>();

        pageImages[1].sprite  = outcomeInfo.CoverImage;
        pageImages[2].sprite  = outcomeInfo.AuthorImage;

        TextMeshProUGUI[] pageText = pages[0].GetComponentsInChildren<TextMeshProUGUI>();

        pageText[0].text = outcomeInfo.HeadlineText;
        pageText[1].text = "Writer: " + outcomeInfo.authorName;
        pageText[2].text = outcomeInfo.publishDate;
        pageText[3].text = outcomeInfo.detailsText;

        VerticalLayoutGroup Comments = pages[0].GetComponentInChildren<VerticalLayoutGroup>();

        foreach (NewsComment comment in outcomeInfo.comments)
        {
            GameObject commentInstance = Instantiate(commentPrefab, Comments.transform);
            TextMeshProUGUI[] commentText = commentInstance.GetComponentsInChildren<TextMeshProUGUI>();

            commentText[0].text = comment.Username;
            commentText[1].text = comment.Comment;

            int randHour = Random.Range(0, 23);
            int randMinutes = Random.Range(0, 59);
            commentText[2].text = (randHour < 10 ? "0" : "") + randHour + ":" + (randMinutes < 10 ? "0" : "") + randMinutes;

            Image userIcon = commentInstance.GetComponentInChildren<Image>();

            userIcon.sprite = comment.Icon;
        }
    }

    private void OnLevelComplete(LevelCompleteEvent levelCompleteEvent)
    {
        ParseStampResult(levelCompleteEvent.StampResults);

        OpenNews();
    }

    private void OpenNews()
    {
        DisableAllPages();
        EnableCurrentPage();

        if (currentPageIndex > 0) { 
            EnablePreviousButton();
        }

        if (currentPageIndex < pages.Count - 1)
        {
            EnableNextButton();
        }

        if (currentPageIndex >= pages.Count - 1)
        {
            EnableNextLevelButton();
        }
    }

    private void DisableAllPages()
    {
        foreach (GameObject page in pages)
        {
            page?.SetActive(false);
        }
    }

    private void EnableCurrentPage()
    {
        pages[currentPageIndex]?.SetActive(true);
    }

    private void DisableCurrentPage()
    {
        pages[currentPageIndex]?.SetActive(false);
    }

    private void EnablePreviousButton()
    {
        previousPageButton?.SetActive(true);
    }

    private void DisablePreviousButton()
    {
        previousPageButton?.SetActive(false);
    }

    private void EnableNextButton()
    {
        nextPageButton?.SetActive(true);
    }

    private void DisableNextButton()
    {
        nextPageButton?.SetActive(false);
    }

    private void EnableNextLevelButton()
    {
        nextLevelButton?.SetActive(true);
    }

    private void DisableNextLevelButton()
    {
        nextLevelButton?.SetActive(false);
    }

    private void DisableAll()
    {
        DisableAllPages();
        DisablePreviousButton();
        DisableNextButton();
        DisableNextLevelButton();
    }

    public void GoToNextPage()
    {
        if (currentPageIndex >= pages.Count - 1) { return; }

        DisableCurrentPage();

        currentPageIndex++;

        EnableCurrentPage();

        EnablePreviousButton();

        if(currentPageIndex >= pages.Count - 1)
        {
            EnableNextLevelButton();
            DisableNextButton();
        }
    }

    public void GoToPreviousPage()
    {
        if (currentPageIndex <= 0) { return; }

        DisableCurrentPage();

        currentPageIndex--;

        EnableCurrentPage();

        EnableNextButton();

        if (currentPageIndex == 0)
        {
            DisablePreviousButton();
        }
    }

    public void ContinueToNextLevel()
    {
        TryLoadNextScene();
    }

    private void TryLoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
