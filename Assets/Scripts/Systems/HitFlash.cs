using UnityEngine;
using System.Collections;
using System.Linq;

public class HitFlash : MonoBehaviour
{
    [Header("Flash Settings")]
    public int flashes = 2;                 // số lần nhấp nháy
    public float singleFlashTime = 0.12f;   // thời gian 1 nhịp (tắt/bật)
    public bool invincibleDuringFlash = false; // vô địch trong lúc nháy?

    [Header("Mode")]
    public bool useColorTint = true;        // true: đổi màu; false: chớp tắt renderer
    public Color flashColor = Color.white;  // màu nháy (nếu dùng tint)

    SpriteRenderer[] renderers;
    Color[] originalColors;
    Collider2D[] cols;
    Health hp;

    void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>(true);
        originalColors = renderers.Select(r => r.color).ToArray();
        cols = GetComponentsInChildren<Collider2D>(true);
        hp = GetComponent<Health>();
    }

    void OnEnable()
    {
        if (hp != null) hp.onDamaged.AddListener(OnDamaged);
    }

    void OnDisable()
    {
        if (hp != null) hp.onDamaged.RemoveListener(OnDamaged);
    }

    void OnDamaged(int cur, int max)
    {
        StopAllCoroutines();
        StartCoroutine(CoFlash());
    }

    IEnumerator CoFlash()
    {
        if (invincibleDuringFlash) SetColliders(false);

        for (int i = 0; i < flashes; i++)
        {
            // Bật flash
            if (useColorTint) SetColorAll(flashColor);
            else SetRendererEnabled(false);

            yield return new WaitForSeconds(singleFlashTime * 0.5f);

            // Tắt flash (trở lại bình thường)
            if (useColorTint) RestoreColors();
            else SetRendererEnabled(true);

            yield return new WaitForSeconds(singleFlashTime * 0.5f);
        }

        if (invincibleDuringFlash) SetColliders(true);
    }

    void SetColorAll(Color c)
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].color = c;
    }

    void RestoreColors()
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].color = originalColors[i];
    }

    void SetRendererEnabled(bool value)
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].enabled = value;
    }

    void SetColliders(bool enabled)
    {
        foreach (var c in cols) c.enabled = enabled;
    }
}
