using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinMenuController : MonoBehaviour
{
    [SerializeField] Canvas overlay;
    public void RestartGame()
    {        
        gameObject.SetActive(false);
        overlay.gameObject.SetActive(false);
    }

    // Function to quit the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
        #else
            Application.Quit(); // Quit the game in a built version
        #endif
    }
}
