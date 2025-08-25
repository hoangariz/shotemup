using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Ra khỏi màn hình thì tắt
        var cam = Camera.main;
        if (cam)
        {
            float vert = cam.orthographicSize;
            if (transform.position.y < -vert - 1f)
                Destroy(gameObject);
        }
    }
}
