using UnityEngine;
using UnityEngine.UI;

public class OpenMenuTemp : MonoBehaviour
{
    public Button menuButton;
    void Start() => menuButton.onClick.AddListener(() => Debug.Log("Open Menu"));
}
