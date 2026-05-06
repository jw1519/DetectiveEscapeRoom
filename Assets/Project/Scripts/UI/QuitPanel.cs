using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : BasePanel
{
    public void Quit()
    {
        GameManager.Instance.EndGame();
        SceneManager.LoadScene("MainMenu"); //starting a new game doesnt work to restart game
    }
}
