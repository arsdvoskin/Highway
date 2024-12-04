using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private ParticleSystem _onCollectEffect;

    private void Start()
    {
        RaceController.Register(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        Object.Instantiate(_onCollectEffect, transform.position, transform.rotation);

        RaceController.MarkCollectableAsGathered(this);
        SoundManager.PlayCollectableSound();
        Destroy(gameObject);
    }
}