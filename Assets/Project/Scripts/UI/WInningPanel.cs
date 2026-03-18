using UnityEngine.SceneManagement;

public class WinningPanel : BasePanel
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
