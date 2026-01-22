using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject leftButton;
    public GameObject rightButton;
    private void Start()
    {
        if (GyroManager.Instance.isGyroActive)
        {
            DisableButtons();
            enabled = false;
        }
        ZoomManager.Instance.onZoomIn += DisableButtons;
        ZoomManager.Instance.onZoomOut += EnableButtons;
    }
    private void OnEnable()
    {
        if (ZoomManager.Instance == null) return;
        ZoomManager.Instance.onZoomIn += DisableButtons;
        ZoomManager.Instance.onZoomOut += EnableButtons;
    }
    private void OnDisable()
    {
        ZoomManager.Instance.onZoomIn -= DisableButtons;
        ZoomManager.Instance.onZoomOut -= EnableButtons;
    }
    public void TurnLeft()
    {
        gameObject.transform.Rotate(0, -90, 0);
    }
    public void TurnRight()
    {
        gameObject.transform.Rotate(0, 90, 0);
    }
    public void DisableButtons()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }
    public void EnableButtons()
    {
        if (GyroManager.Instance.isGyroActive) return;
        leftButton.SetActive(true);
        rightButton.SetActive(true);
    }
}
