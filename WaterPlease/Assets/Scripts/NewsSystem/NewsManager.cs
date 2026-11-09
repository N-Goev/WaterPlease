using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewsManager : MonoBehaviour
{
    [SerializeField] private GameObject previousPageButton;
    [SerializeField] private GameObject nextPageButton;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private List<GameObject> pages;

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
        DisableAll();
    }

    private void OnLevelComplete(LevelCompleteEvent levelCompleteEvent)
    {
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
