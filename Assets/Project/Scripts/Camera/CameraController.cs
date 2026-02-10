using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<CinemachineCamera> cameras;
    CinemachineCamera currentCamera;
    public int currentCameraIndex = 0;
    CinemachineCamera nextCamera;
    CinemachineCamera previousCamera;

    public CinemachineCamera inspectCamera;
    public CinemachineCamera ZoomCamera;

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
    }
    public void EnableInspect()
    {
        previousCamera = currentCamera;
        currentCamera.gameObject.SetActive(false);
        currentCamera = inspectCamera;
        inspectCamera.gameObject.SetActive(true);
        DisableButtons();
    }
    public void DisableInspect()
    {
        previousCamera.gameObject.SetActive(true);
        currentCamera = previousCamera;
        inspectCamera.gameObject.SetActive(false);
        if (previousCamera == cameras[currentCameraIndex])
        {
            EnableButtons();
        }
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
    public void EnableZoom()
    {
        previousCamera = currentCamera;
        currentCamera.gameObject.SetActive(false);
        currentCamera = ZoomCamera;
        ZoomCamera.gameObject.SetActive(true);
        DisableButtons();
    }
    public void DisableZoom()
    {
        currentCamera = cameras[currentCameraIndex];
        currentCamera.gameObject.SetActive(true);
        ZoomCamera.gameObject.SetActive(false);
        EnableButtons();
    }
    public void DisableAllCameras()
    {
        foreach (CinemachineCamera cam in cameras)
        {
            cam.gameObject.SetActive(false);
        }
        inspectCamera.gameObject.SetActive(false);
        ZoomCamera.gameObject.SetActive(false);
        DisableButtons();
    }
}
