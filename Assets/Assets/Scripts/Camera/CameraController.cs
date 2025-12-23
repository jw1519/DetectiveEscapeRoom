using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject leftButton;
    public GameObject rightButton;
    private void OnEnable()
    {
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
        leftButton.SetActive(true);
        rightButton.SetActive(true);
    }
}
