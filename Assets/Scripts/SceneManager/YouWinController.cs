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
        Application.Quit(); 
    }
}
