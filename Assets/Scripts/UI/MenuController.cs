using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Tên scene (đúng 100%, KHÔNG có .unity)")]
    public string playScene      = "Game";
    public string highscoreScene = "Highscore";
    public string settingsScene  = "Settings";
    public string helpScene      = "Help";

    [Header("Popup thoát (gán GameObject popup)")]
    public GameObject quitPopup;   // để trống thì sẽ thoát app luôn

    [Header("Âm thanh click (tuỳ chọn)")]
    public AudioSource sfxSource;
    public AudioClip clickClip;

    void Click() {
        if (sfxSource && clickClip) sfxSource.PlayOneShot(clickClip);
    }

    // ===== Nút bấm =====
    public void OnPlay()      { Click(); TryLoad(playScene); }
    public void OnHighscore() { Click(); TryLoad(highscoreScene); }
    public void OnSettings()  { Click(); TryLoad(settingsScene); }
    public void OnHelp()      { Click(); TryLoad(helpScene); }

    public void OnQuit() {
        Click();
        if (quitPopup) quitPopup.SetActive(true);
        else QuitNow(); // nếu chưa làm popup thì thoát luôn
    }

    // ===== Popup Quit =====
    public void QuitYes() { QuitNow(); }
    public void QuitNo()  { if (quitPopup) quitPopup.SetActive(false); }

    void QuitNow() {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // ===== Tiện ích =====
    public void LoadSceneByName(string sceneName) { Click(); TryLoad(sceneName); } // dùng 1 hàm chung nếu thích

    void TryLoad(string sceneName) {
        if (!IsInBuild(sceneName)) {
            Debug.LogError($"[Menu] Scene '{sceneName}' chưa có trong Build Settings hoặc sai tên.");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }

    bool IsInBuild(string name) {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
            var path = SceneUtility.GetScenePathByBuildIndex(i);
            if (System.IO.Path.GetFileNameWithoutExtension(path) == name) return true;
        }
        return false;
    }
}
