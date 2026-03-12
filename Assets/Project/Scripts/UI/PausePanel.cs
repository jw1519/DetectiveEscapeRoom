using UnityEngine;

public class PausePanel : BasePanel
{
    public override void OpenPanel()
    {
        base.OpenPanel();
        Time.timeScale = 0f;
    }
    public override void ClosePanel()
    {
        base.ClosePanel();
        Time.timeScale = 1f;
    }
    public void Settings()
    {
        ManagerUI.Instance.OpenPanel("SettingsPanel");
    }
    public void Quit()
    {
        ManagerUI.Instance.OpenPanel("QuitPanel");
    }
}
