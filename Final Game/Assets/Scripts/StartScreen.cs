using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void LoadNextScene()
    {
        // creates index to find active scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // load the next scene for Level 1
        SceneManager.LoadScene("SampleScene");

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
