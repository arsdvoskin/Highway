using System.Collections.Generic;
using UnityEngine;

public class GhostCar : MonoBehaviour
{
    [SerializeField] private Renderer[] _renderers;
    private List<CarPositionData> _carPositions;
    private int _currentIndex = 0;
    private float _currentTimerDelta;
    private float _timer = 0f;

    private CarPositionData _CurrentPositionData => _carPositions[_currentIndex];
    private CarPositionData _NextPositionData => _carPositions[_currentIndex + 1];

    private void Awake()
    {
        if (!ES3.KeyExists("BestTimeCarPositions"))
        {
            Destroy(gameObject);
            return;
        }

        _renderers = GetComponentsInChildren<Renderer>();
        enabled = false;
    }

    private void OnDisable()
    {
        foreach (var renderer in _renderers)
        {
            renderer.enabled = false;
        }
    }

    private void OnEnable()
    {
        foreach (var renderer in _renderers)
        {
            renderer.enabled = true;
        }
    }

    private void Start()
    {
        _carPositions = ES3.Load<List<CarPositionData>>("BestTimeCarPositions");
        RecalculateTimerDelta();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _currentTimerDelta)
        {
            _timer = 0f;
            _currentIndex++;

            if (_currentIndex >= _carPositions.Count - 1)
            {
                enabled = false;
                return;
            }

            RecalculateTimerDelta();
        }

        float lerpFraction = _timer / _currentTimerDelta;

        transform.position = Vector3.Lerp(_CurrentPositionData.position, _NextPositionData.position, lerpFraction);
        transform.rotation = Quaternion.Lerp(_CurrentPositionData.rotation, _NextPositionData.rotation, lerpFraction);
    }

    private void RecalculateTimerDelta()
    {
        _currentTimerDelta = _NextPositionData.time - _CurrentPositionData.time;
    }
}