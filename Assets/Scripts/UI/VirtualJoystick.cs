using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("Setup")]
    public RectTransform handle;   // núm tròn
    public float radius = 150f;    // bán kính tối đa

    private Vector2 input;         // giá trị -1..1
    private RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        if (handle) handle.anchoredPosition = Vector2.zero;
    }

    public Vector2 Read() => input;

    public void OnPointerDown(PointerEventData e) => OnDrag(e);

    public void OnPointerUp(PointerEventData e)
    {
        input = Vector2.zero;
        if (handle) handle.anchoredPosition = Vector2.zero;
    }

    public void OnDrag(PointerEventData e)
    {
        if (rect == null) return;

        // đổi vị trí chuột/touch sang local trong RectTransform
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect, e.position, e.pressEventCamera, out var pos);

        // giới hạn trong vòng tròn bán kính radius
        var v = Vector2.ClampMagnitude(pos, radius);
        input = v / radius;

        if (handle) handle.anchoredPosition = v;
    }
}
