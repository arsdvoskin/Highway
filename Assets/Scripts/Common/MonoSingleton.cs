using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static T _instance;
    public static T Instance => _instance;

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            Init();
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Second instance of {typeof(T)} created. Automatic self-destruct triggered.");
#endif
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            OnInstanceDestroy();
        }
    }

    protected virtual void Init() {}
    protected virtual void OnInstanceDestroy() {}
}
