using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;   // kéo Bullet_Player.prefab vào đây
    public Transform firePoint;       // kéo FirePoint (Empty con của Player) vào đây
    public float bulletSpeed = 12f;   // tốc độ đạn (world units/giây)
    public float fireInterval = 0.2f; // 0.2s = 5 viên/giây

    float timer;

    void Update()
    {
        if (Time.timeScale == 0f) return;                 // đang pause thì không bắn
        if (!bulletPrefab || !firePoint) return;          // thiếu tham chiếu thì thôi

        timer += Time.deltaTime;
        if (timer >= Mathf.Max(0.01f, fireInterval))
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        // Bắn thẳng lên: rotation = identity, velocity = Vector2.up
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        var rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.up * bulletSpeed; // đẩy theo trục Y của thế giới
            rb.gravityScale = 0f;
        }
        else
        {
            Debug.LogWarning("Bullet prefab thiếu Rigidbody2D (Dynamic, Gravity=0).");
        }
    }
}
