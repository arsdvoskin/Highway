using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] private AudioSource _collectableSource;

    private static AudioSource _CollectableSource => _instance._collectableSource;

    public static void PlayCollectableSound()
    {
        _CollectableSource.Play();
    }
}