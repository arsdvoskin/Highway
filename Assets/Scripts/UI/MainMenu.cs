using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartStory()
    {
        SceneManager.LoadScene((int)Scenes.Story, LoadSceneMode.Single);
    }
    
    public void StartRace()
    {
        SceneManager.LoadScene((int)Scenes.Race, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
