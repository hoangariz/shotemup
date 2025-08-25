using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyEntry {
    public GameObject prefab;
    [Range(0f,1f)] public float weight = 1f; // xác suất tương đối
}

public class MultiEnemySpawner : MonoBehaviour
{
    public EnemyEntry[] enemies;

    [Header("Spawn settings")]
    public float spawnInterval = 0.7f;
    public float topOffset = 0.5f;
    public float sidePadding = 0.3f;

    Camera cam;

    void Start(){
        cam = Camera.main;
        StartCoroutine(Loop());
    }

    IEnumerator Loop(){
        while(true){
            if (Time.timeScale > 0f) SpawnOne();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnOne(){
        if (enemies == null || enemies.Length == 0) return;
        var prefab = PickWeighted();
        if (!prefab || !cam) return;

        float vert = cam.orthographicSize;
        float horz = vert * cam.aspect;
        float x = Random.Range(-horz + sidePadding, horz - sidePadding);
        float y = vert + Mathf.Abs(topOffset);

        Instantiate(prefab, new Vector3(x, y, 0f), Quaternion.identity);
    }

    GameObject PickWeighted(){
        float sum = 0f;
        foreach (var e in enemies) sum += Mathf.Max(0f, e.weight);
        if (sum <= 0f) return enemies[0].prefab;
        float r = Random.value * sum;
        foreach (var e in enemies){
            r -= Mathf.Max(0f, e.weight);
            if (r <= 0f) return e.prefab;
        }
        return enemies[enemies.Length-1].prefab;
    }
}
