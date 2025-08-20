using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 8f;
    public VirtualJoystick joystick;   // Kéo JoystickArea vào đây

    [Header("UI padding (pixels) - nhập tay")]
    [SerializeField] float topBarPx = 180f;     // = Height của TopBar trong Canvas
    [SerializeField] float bottomBarPx = 260f;  // = Height của BottomBar trong Canvas
    [SerializeField] float leftPadPx = 0f;      // nếu muốn chừa lề trái
    [SerializeField] float rightPadPx = 0f;     // nếu muốn chừa lề phải

    [Header("Extra range (world units)")]
    [SerializeField] float extraXRange = 2f;    // nới ngang thêm mỗi bên
    [SerializeField] float extraYRange = 0f;    // thường để 0

    [Header("Debug")]
    [SerializeField] bool drawBounds = true;

    Rigidbody2D rb;
    Camera cam;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Start()
    {
        // Snap player vào trong khung hợp lệ lúc bắt đầu
        Bounds2D b = CalcBounds();
        Vector3 p = transform.position;
        p.x = Mathf.Clamp(p.x, b.minX + 0.5f, b.maxX - 0.5f);
        p.y = Mathf.Clamp(p.y, b.minY + 0.5f, b.maxY - 0.5f);
        transform.position = p;
    }

    void Update()
    {
        // 1) Input
        Vector2 dir = joystick ? joystick.Read() : Vector2.zero;

        // 2) Move
        rb.linearVelocity = dir.normalized * speed;

        // 3) Clamp
        Bounds2D b = CalcBounds();
        Vector3 p = transform.position;
        p.x = Mathf.Clamp(p.x, b.minX, b.maxX);
        p.y = Mathf.Clamp(p.y, b.minY, b.maxY);
        transform.position = p;
    }

    struct Bounds2D { public float minX, maxX, minY, maxY; }

    Bounds2D CalcBounds()
    {
        float vert = cam.orthographicSize;          // nửa chiều cao (world)
        float horz = vert * cam.aspect;             // nửa chiều rộng (world)

        // Base theo camera
        float minX = -horz, maxX = horz;
        float minY = -vert, maxY = vert;

        // Quy đổi px UI -> world units
        float worldTop    = (topBarPx    / Screen.height) * (2f * vert);
        float worldBottom = (bottomBarPx / Screen.height) * (2f * vert);
        float worldLeft   = (leftPadPx   / Screen.width)  * (2f * horz);
        float worldRight  = (rightPadPx  / Screen.width)  * (2f * horz);

        // Trừ vùng UI
        minY += worldBottom;
        maxY -= worldTop;
        minX += worldLeft;
        maxX -= worldRight;

        // Nới biên (nếu muốn)
        minX -= extraXRange;
        maxX += extraXRange;
        minY -= extraYRange;
        maxY += extraYRange;

        return new Bounds2D { minX = minX, maxX = maxX, minY = minY, maxY = maxY };
    }

    void OnDrawGizmosSelected()
    {
        if (!drawBounds) return;
        if (cam == null) cam = Camera.main;
        if (cam == null) return;

        Bounds2D b = CalcBounds();
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector3(b.minX, b.minY), new Vector3(b.maxX, b.minY));
        Gizmos.DrawLine(new Vector3(b.minX, b.maxY), new Vector3(b.maxX, b.maxY));
        Gizmos.DrawLine(new Vector3(b.minX, b.minY), new Vector3(b.minX, b.maxY));
        Gizmos.DrawLine(new Vector3(b.maxX, b.minY), new Vector3(b.maxX, b.maxY));
    }
}
