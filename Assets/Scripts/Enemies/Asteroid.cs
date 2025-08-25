using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Ra khỏi màn hình thì tự hủy
        var cam = Camera.main;
        if (cam)
        {
            float vert = cam.orthographicSize;
            if (transform.position.y < -vert - 1f)
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var hp = other.GetComponent<Health>();
            if (hp) hp.TakeDamage(1);

            // nếu muốn asteroid biến mất khi đụng player
            Destroy(gameObject);
        }
    }
}
