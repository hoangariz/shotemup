using UnityEngine;

public class VFXSpawner : MonoBehaviour
{
    public GameObject explosionPrefab; // gán VFX_Explosion01 vào đây
    public void SpawnAt(Transform t)
    {
        if (!explosionPrefab || !t) return;
        Instantiate(explosionPrefab, t.position, Quaternion.identity);
    }
}
