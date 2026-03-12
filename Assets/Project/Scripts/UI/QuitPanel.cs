using UnityEngine.SceneManagement;

public class QuitPanel : BasePanel
{
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
