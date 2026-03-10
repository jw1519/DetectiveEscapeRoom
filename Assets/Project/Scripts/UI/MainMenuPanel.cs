using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPanel : BasePanel
{
    public void StartGame()
    {
        ClosePanel();
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
