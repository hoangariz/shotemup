using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 18f;      // world units/giây
    public float lifeTime = 3f;    // tự hủy sau N giây
    float t0;

    void OnEnable()
    {
        t0 = Time.time;
        // Debug nhanh để biết có chạy OnEnable không
        // Debug.Log($"Bullet spawn @ {transform.position}, timeScale={Time.timeScale}");
    }

    void Update()
    {
        // Nếu đang pause (timeScale=0) thì không đi — bỏ dòng này nếu muốn đạn vẫn bay khi pause
        if (Time.timeScale == 0f) return;

        // Bay thẳng lên theo trục Y của thế giới (không phụ thuộc xoay prefab)
        transform.position += Vector3.up * speed * Time.deltaTime;

        // Tự hủy sau lifeTime
        if (Time.time - t0 > lifeTime)
            Destroy(gameObject);
    }
}
