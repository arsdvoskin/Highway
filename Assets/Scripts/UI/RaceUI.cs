using UnityEngine;
using TMPro;

public class RaceUI : MonoSingleton<RaceUI>
{
    [SerializeField] private TMP_Text _countdownText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _collectablesCounterText;
    [SerializeField] private EscMenu _escMenu;

    protected override void Init()
    {
        _countdownText.gameObject.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _escMenu.gameObject.SetActive(!_escMenu.gameObject.activeSelf);
        }
    }

    public static void SetCountdownText(string text)
    {
        _instance._countdownText.text = text;
    }

    public static void HideCountdownText()
    {
        _instance._countdownText.gameObject.SetActive(false);
    }

    public static void SetTimerText(float value)
    {
        float roundedValue = Mathf.Round(value * 100f) / 100f;
        _instance._timerText.text = $"Time: {roundedValue}";
    }

    public static void UpdateCollectablesInfo()
    {
        _instance._collectablesCounterText.text = $"{RaceController.GatheredCollectablesQuantity} / {RaceController.CollectablesQuantity}";
    }
}
