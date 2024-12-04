using UnityEngine;

public class SetSkyboxTrigger : MonoBehaviour
{
    [SerializeField] private Material skyboxMaterial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RenderSettings.skybox = skyboxMaterial;
            DynamicGI.UpdateEnvironment();
            RenderSettings.reflectionIntensity = 1f;
        }
    }
}
