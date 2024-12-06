using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame() {
        SceneManager.LoadScene("SampleScene"); 
    }

    public void YouWin() {
        SceneManager.LoadScene("YouWin"); 
    }
    public void RestartGame() {
        SceneManager.LoadScene("SampleScene"); 
    }

    public void QuitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
        #else
            Application.Quit(); // Quit the application in the build
        #endif
    }
}
