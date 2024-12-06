using UnityEngine;
using UnityEngine.SceneManagement; // For scene loading

public class StartMenuController : MonoBehaviour
{
    
    public void StartGame()
    {
        
        SceneManager.LoadScene("SampleScene");
    }

    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}