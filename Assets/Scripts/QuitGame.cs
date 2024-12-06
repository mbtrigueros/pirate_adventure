using UnityEngine;

public class QuitGame : MonoBehaviour
{

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Quit();
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}