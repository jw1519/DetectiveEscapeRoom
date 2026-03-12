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
        }
        ZoomManager.Instance.onZoomIn += EnableZoom;
        ZoomManager.Instance.onZoomOut += DisableZoom;

        currentCamera = cameras.Find(camera => camera.gameObject.activeSelf);
        currentCameraIndex = cameras.IndexOf(currentCamera);
        inspectCamera.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if (ZoomManager.Instance == null) return;
        ZoomManager.Instance.onZoomIn += EnableZoom;
        ZoomManager.Instance.onZoomOut += DisableZoom;
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
        if (GyroManager.Instance.isGyroActive)
        {
            DisableAllCameras();
            return;
        }
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
        if (leftButton != null)
            leftButton.SetActive(true);
        if (rightButton != null)
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
        if (GyroManager.Instance.isGyroActive)
        {
            ZoomCamera.gameObject.SetActive(false);
            return;
        }
        currentCamera = cameras[currentCameraIndex];
        if (currentCamera != null)
            currentCamera.gameObject.SetActive(true);
        if (ZoomCamera != null)
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
