using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _collectablesText;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (_timeText != null)
        {
            float time = ES3.Load<float>("LastRaceTime");
            float roundedTime = Mathf.Round(time * 1000f) / 1000f;
            _timeText.text = $"Time: {roundedTime}";
        }

        if (_collectablesText != null)
        {
            _collectablesText.text = ES3.Load<string>("LastRaceCollectablesInfo");
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene((int)Scenes.Race, LoadSceneMode.Single);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene((int)Scenes.TitleScreen, LoadSceneMode.Single);
    }
}
