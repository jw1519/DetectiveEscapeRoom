using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManagerUI : MonoBehaviour
{
    public static ManagerUI Instance;

    public List<BasePanel> panels;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void RegisterPanel(BasePanel panel)
    {
        if (!panels.Contains(panel))
        {
            panels.Add(panel);
        }
    }
    public bool IsTouchOverUI(Vector2 screenPos)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = screenPos;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }
    public void CloseAllPanels()
    {
        foreach (BasePanel panel in panels)
        {
            panel.ClosePanel();
        }
    }
    public void OpenPanel(string panel)
    {
        foreach (BasePanel p in panels)
        {
            if (p.name == panel)
            {
                p.OpenPanel();
                break;
            }
        }
    }
}
