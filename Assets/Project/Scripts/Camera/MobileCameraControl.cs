using UnityEngine;

public class MobileCameraControl : MonoBehaviour
{
    public int movementSpeed;
    private void Start()
    {
        if (!GyroManager.Instance.isGyroActive)
        {
            enabled = false;
        }
        else
        {
            CameraController camController = GetComponent<CameraController>();
            camController.DisableAllCameras();
        }
    }
    private void FixedUpdate()
    {
        if (GyroManager.Instance.isGyroActive)
            transform.localRotation = GyroManager.Instance.rotation;
    }
}
