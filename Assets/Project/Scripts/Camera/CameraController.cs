using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<CinemachineCamera> cameras;
    CinemachineCamera currentCamera;
    public int currentCameraIndex = 0;
    CinemachineCamera nextCamera;

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

        currentCamera = cameras.Find(camera => camera.gameObject.activeSelf);
        currentCameraIndex = cameras.IndexOf(currentCamera);
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
        currentCamera.gameObject.SetActive(false);
        currentCameraIndex--;
        if (currentCameraIndex < 0)
        {
            nextCamera = cameras[cameras.Count - 1];
            currentCameraIndex = cameras.Count - 1;
        }
        else
            nextCamera = cameras[currentCameraIndex];

        nextCamera.gameObject.SetActive(true);
        currentCamera = nextCamera;
        //gameObject.transform.Rotate(0, -90, 0);
    }
    public void TurnRight()
    {
        currentCamera.gameObject.SetActive(false);
        currentCameraIndex++;
        if (currentCameraIndex > cameras.Count - 1)
        {
            nextCamera = cameras[0];
            currentCameraIndex = 0;
        }
        else
            nextCamera = cameras[currentCameraIndex];

        nextCamera.gameObject.SetActive(true);
        currentCamera = nextCamera;
        //gameObject.transform.Rotate(0, 90, 0);
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
