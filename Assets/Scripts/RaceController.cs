using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceController : MonoSingleton<RaceController>
{
    [field: SerializeField]
    public MonoBehaviour PlayerController { get; private set; }

    [SerializeField] private CarPositionsRecorder _positionsRecorder;
    [SerializeField] private GhostCar _ghost;

    private int _collectablesQuantity = 0;
    private int _gatheredCollectablesQuantity = 0;

    public static event Action OnBeforeFinish;
    public static event Action OnNewRecord;

    private float _timer = 0f;
    public static float Timer => _instance._timer;

    public static int CollectablesQuantity => _instance._collectablesQuantity;
    public static int GatheredCollectablesQuantity => _instance._gatheredCollectablesQuantity;

    protected override void Init()
    {
        Application.targetFrameRate = -1;

        enabled = false;
        PlayerController.enabled = false;
    }

    protected override void OnInstanceDestroy()
    {
        OnBeforeFinish = null;
        OnNewRecord = null;
    }

    private void OnEnable()
    {
        PlayerController.enabled = true;
        _positionsRecorder.enabled = true;

        if (_ghost != null)
        {
            _ghost.enabled = true;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        RaceUI.SetTimerText(_timer);
    }

    public static void Finish(bool success = true)
    {
        OnBeforeFinish?.Invoke();

        if (!success)
        {
            SceneManager.LoadScene((int)Scenes.EndingB, LoadSceneMode.Single);
            return;
        }

        ES3.Save("LastRaceTime", _instance._timer);

        if (!ES3.KeyExists("BestTime") || ES3.Load<float>("BestTime") > _instance._timer)
        {
            ES3.Save("BestTime", _instance._timer);
            OnNewRecord?.Invoke();
        }

        ES3.Save("LastRaceCollectablesInfo", $"{GatheredCollectablesQuantity} / {CollectablesQuantity}");

        if (GatheredCollectablesQuantity == CollectablesQuantity)
        {
            SceneManager.LoadScene((int)Scenes.EndingC, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene((int)Scenes.EndingA, LoadSceneMode.Single);
        }
    }

    public static void Register(Collectable collectable)
    {
        _instance._collectablesQuantity++;
        RaceUI.UpdateCollectablesInfo();
    }

    public static void MarkCollectableAsGathered(Collectable collectable)
    {
        _instance._gatheredCollectablesQuantity++;
        RaceUI.UpdateCollectablesInfo();
    }
}
