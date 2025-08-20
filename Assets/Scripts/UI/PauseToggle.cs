using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseToggle : MonoBehaviour
{
    public Button pauseButton;
    public TMP_Text label; // text trên nút, nếu dùng icon thì để null

    bool paused = false;

    void Start()
    {
        pauseButton.onClick.AddListener(Toggle);
        UpdateLabel();
    }

    void Toggle()
    {
        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;
        UpdateLabel();
    }

    void UpdateLabel()
    {
        if (label) label.text = paused ? "▶" : "||";
    }
}
