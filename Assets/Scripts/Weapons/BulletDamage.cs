using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damage = 1;
    [Tooltip("Tag của mục tiêu mà đạn sẽ gây sát thương")]
    public string targetTag = "Enemy";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other || !other.CompareTag(targetTag)) return;

        var hp = other.GetComponent<Health>();
        if (hp) hp.TakeDamage(damage);

        Destroy(gameObject); // đạn biến mất sau khi chạm
    }
}
