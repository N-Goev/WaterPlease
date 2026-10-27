using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuHandler : MonoBehaviour
{
    public void StartGame()
    {
        TryLoadNextScene();
    }

    public void QuitGame()
    {
        Application.Quit();
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
