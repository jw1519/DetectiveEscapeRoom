using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPanel : MonoBehaviour
{
    public void StartGame()
    {

        SceneManager.LoadScene("GameScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void OpenSettings()
    {
        // Implement settings panel opening logic here
    }
}
