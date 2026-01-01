using UnityEngine;

public interface ISpawnable
{
    string ObjectName { get; set; }
    GameObject SpawnParticleSystem { get; set; }

    void SpawnWithTimer(GameObject particleSystemPrefab, Vector3 position, Quaternion rotation, float duration = 3f);
}

public abstract class BaseEntity : MonoBehaviour, ISpawnable
{
    public string ObjectName { get; set; }
    public GameObject SpawnParticleSystem { get; set; }

    public void SpawnWithTimer(GameObject particleSystemPrefab, Vector3 position, Quaternion rotation, float duration = 3f)
    {
        if (particleSystemPrefab != null)
        {
            GameObject particleSystemGameObject = Instantiate(particleSystemPrefab, position, rotation);
            Destroy(particleSystemGameObject, duration);
        }
    }
}