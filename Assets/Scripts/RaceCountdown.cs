using UnityEngine;

public class RaceCountdown : MonoBehaviour
{
    [SerializeField] private float _interval;
    [SerializeField, Min(1)] private int _startNumber;
    [SerializeField] private string _finalText;
    private float _timer = 0f;
    private int _currentNumber;

    private void Start()
    {
        _currentNumber = _startNumber;
        RaceUI.SetCountdownText(_currentNumber.ToString());
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer < _interval)
        {
            return;
        }

        _timer = 0f;
        _currentNumber--;

        switch (_currentNumber)
        {
            case 0:
                RaceUI.SetCountdownText(_finalText);
                break;
            

            case -1:
                RaceUI.HideCountdownText();
                RaceController.Instance.enabled = true;
                Destroy(this);
                break;
            
            default:
                RaceUI.SetCountdownText(_currentNumber.ToString());
                break;
        }
    }
}
