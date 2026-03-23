using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManagerUI : MonoBehaviour
{
    public static ManagerUI Instance;

    public List<BasePanel> panels;

    public GameObject onScreenCameraControls;
    public TextMeshProUGUI hintText;
    public int hintDisplayDuration = 3;
    float hintTimer;

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
    private void Start()
    {
        if (GyroManager.Instance != null)
        {
            GyroManager.Instance.onGyroDisable += EnableOnScreenCameraControls;
            GyroManager.Instance.onGyroEnable += DisableOnScreenCameraControls;
        }
        if (ZoomManager.Instance != null)
        {
            ZoomManager.Instance.onZoomIn += DisableOnScreenCameraControls;
            ZoomManager.Instance.onZoomOut += EnableOnScreenCameraControls;
        }
    }
    private void OnEnable()
    {
        if (GyroManager.Instance != null)
        {
            GyroManager.Instance.onGyroDisable += EnableOnScreenCameraControls;
            GyroManager.Instance.onGyroEnable += DisableOnScreenCameraControls;
        }
        if (ZoomManager.Instance != null)
        {
            ZoomManager.Instance.onZoomIn += DisableOnScreenCameraControls;
            ZoomManager.Instance.onZoomOut += EnableOnScreenCameraControls;
        }
    }
    private void OnDisable()
    {
        if (GyroManager.Instance != null)
        {
            GyroManager.Instance.onGyroDisable -= EnableOnScreenCameraControls;
            GyroManager.Instance.onGyroEnable -= DisableOnScreenCameraControls;
        }
        if (ZoomManager.Instance != null)
        {
            ZoomManager.Instance.onZoomIn -= DisableOnScreenCameraControls;
            ZoomManager.Instance.onZoomOut -= EnableOnScreenCameraControls;
        }
    }
    private void Update()
    {
        if (hintText.enabled)
        {
            if (Time.time >= hintTimer)
                hintText.enabled = false;
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
    public void DisableOnScreenCameraControls()
    {
        if (onScreenCameraControls != null)
            onScreenCameraControls.SetActive(false);
    }
    public void EnableOnScreenCameraControls()
    {
        if (onScreenCameraControls != null)
            onScreenCameraControls.SetActive(true);
    }
    public void SetHintText(string text)
    {
        hintTimer = Time.time + hintDisplayDuration;
        hintText.enabled = true;
        if (hintText != null)
        {
            hintText.text = text;
        }
    }
}
