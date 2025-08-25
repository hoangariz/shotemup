using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 2.5f; // chậm hơn enemy
    public float topOffset = 0.5f;
    public float sidePadding = 0.5f;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        while (true)
        {
            SpawnOne();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnOne()
    {
        if (!asteroidPrefab || cam == null) return;

        float vert = cam.orthographicSize;
        float horz = vert * cam.aspect;

        float x = Random.Range(-horz + sidePadding, horz - sidePadding);
        float y = vert + topOffset;

        Instantiate(asteroidPrefab, new Vector3(x, y, 0f), Quaternion.identity);
    }
}
