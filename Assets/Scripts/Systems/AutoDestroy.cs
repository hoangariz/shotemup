using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float life = 0.5f; // thời gian dài bằng animation (9 frame * 0.05s = 0.45s)
    void OnEnable() { Invoke(nameof(Kill), life); }
    void Kill() { Destroy(gameObject); }
}
