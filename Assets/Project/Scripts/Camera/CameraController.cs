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
            ManagerUI.Instance.DisableOnScreenCameraControls();
        }
        ZoomManager.Instance.onZoomIn += EnableZoom;
        ZoomManager.Instance.onZoomOut += DisableZoom;
        GyroManager.Instance.onGyroEnable += DisableAllCameras;

        //GameObject Cameras = GameManager.Instance.Objects.Find(c => c.name == "MainCameras");
        //for (int i = 0; i < Cameras.transform.childCount; i++ )
        //{
        //    if (Cameras.transform.GetChild(i).name == "InspectCamera")
        //    {
        //        inspectCamera = Cameras.transform.GetChild(i).GetComponent<CinemachineCamera>();
        //    }
        //    else if (Cameras.transform.GetChild(i).name == "ZoomInCamera")
        //    {
        //        ZoomCamera = Cameras.transform.GetChild(i).GetComponent<CinemachineCamera>();
        //    }
        //    else
        //    {
        //        cameras.Add(Cameras.transform.GetChild(i).GetComponent<CinemachineCamera>());
        //    }
        //}

        currentCamera = cameras.Find(camera => camera.gameObject.activeSelf);
        currentCameraIndex = cameras.IndexOf(currentCamera);
        inspectCamera.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if (ZoomManager.Instance == null) return;
        ZoomManager.Instance.onZoomIn += EnableZoom;
        ZoomManager.Instance.onZoomOut += DisableZoom;
        GyroManager.Instance.onGyroEnable += DisableAllCameras;
    }
    private void OnDisable()
    {
        ZoomManager.Instance.onZoomIn -= EnableZoom;
        ZoomManager.Instance.onZoomOut -= DisableZoom;
        GyroManager.Instance.onGyroEnable -= DisableAllCameras;
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
        ManagerUI.Instance.DisableOnScreenCameraControls();
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
            ManagerUI.Instance.EnableOnScreenCameraControls();
        }
    }
    public void EnableZoom()
    {
        previousCamera = currentCamera;
        currentCamera.gameObject.SetActive(false);
        currentCamera = ZoomCamera;
        ZoomCamera.gameObject.SetActive(true);
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
    }
    public void DisableAllCameras()
    {
        foreach (CinemachineCamera cam in cameras)
        {
            cam.gameObject.SetActive(false);
        }
        inspectCamera.gameObject.SetActive(false);
        ZoomCamera.gameObject.SetActive(false);
    }
}
