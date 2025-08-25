using UnityEngine;
[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour {
    RectTransform rt; Rect last;
    void Awake(){ rt = GetComponent<RectTransform>(); Apply(); }
    void OnEnable(){ Apply(); }
#if UNITY_EDITOR
    void Update(){ Apply(); }
#endif
    void Apply(){
        var s = Screen.safeArea; if (s == last) return; last = s;
        Vector2 min = s.position, max = s.position + s.size;
        min.x/=Screen.width;  min.y/=Screen.height;
        max.x/=Screen.width;  max.y/=Screen.height;
        rt.anchorMin = min; rt.anchorMax = max;
        rt.offsetMin = rt.offsetMax = Vector2.zero;
    }
}
