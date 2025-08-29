using UnityEngine;

public class QuitPopupUI : MonoBehaviour
{
    public GameObject popupRoot; // gán QuitPopup vào đây

    public void ShowPopup()  { popupRoot.SetActive(true); }
    public void HidePopup()  { popupRoot.SetActive(false); }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
