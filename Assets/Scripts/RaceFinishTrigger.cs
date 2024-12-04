using UnityEngine;

public class RaceFinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RaceController.Finish();
            Destroy(gameObject);
        }
    }
}
