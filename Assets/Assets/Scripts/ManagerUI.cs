using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    public static ManagerUI instance;
    public EventSystem eventSystem;
    public GraphicRaycaster graphicRaycaster;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public bool IsPointerOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);
        if (results.Count == 0)
        {
            return true;
        }
        return false;
    }
}
