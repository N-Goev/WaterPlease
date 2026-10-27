using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuHandler : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
