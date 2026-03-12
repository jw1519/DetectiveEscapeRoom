using UnityEngine;
using UnityEngine.UI;

public class PanelButton : MonoBehaviour
{
    public string panelName = "";
    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OpenPanel);
    }
    public void OpenPanel()
    {
        if (panelName != null)
        {
            ManagerUI.Instance.OpenPanel(panelName);
        }
    }
}
