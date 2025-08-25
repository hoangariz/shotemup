using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHP = 5;
    public UnityEvent onDeath;
    public UnityEvent<int,int> onDamaged; // (hp, maxHP)

    int hp;
    public int Current => hp;

    void Awake() { hp = maxHP; }

    public void TakeDamage(int dmg)
    {
        if (hp <= 0) return;
        hp = Mathf.Max(0, hp - Mathf.Max(0, dmg));
        onDamaged?.Invoke(hp, maxHP);
        if (hp == 0) Die();
    }

    public void Heal(int v)
    {
        hp = Mathf.Min(maxHP, hp + Mathf.Max(0, v));
        onDamaged?.Invoke(hp, maxHP);
    }

    void Die()
    {
        onDeath?.Invoke();          // cho nổ, rơi loot... (gắn trong Inspector)
        Destroy(gameObject);        // xoá object khi chết
    }
}
