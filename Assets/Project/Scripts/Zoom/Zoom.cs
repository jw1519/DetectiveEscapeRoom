using Unity.Cinemachine;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    Collider zoomCollider;
    public int zoomView;
    public Quaternion rotation;
    public Vector3 targetPosition;
    CinemachineCamera cinemachineCamera;
    private void Start()
    {
        zoomCollider = GetComponent<Collider>();
        cinemachineCamera = Camera.main.gameObject.GetComponentInParent<CameraController>().ZoomCamera;
    }

    public void ZoomIn()
    {
        if (zoomCollider != null)
        {
            zoomCollider.enabled = false;
        }
        // Apply target position if specified
        if (targetPosition != Vector3.zero)
        {
            cinemachineCamera.transform.position = targetPosition;
        }
        cinemachineCamera.transform.LookAt(transform, Vector3.up);
        ZoomManager.Instance.RegisterZoom(this);
        cinemachineCamera.Lens.FieldOfView = zoomView;
        // Apply rotation while maintaining other axes
        Vector3 currentRotation = cinemachineCamera.transform.rotation.eulerAngles;
        if (rotation.x != 0)
        {
            currentRotation.x = rotation.x;
        }
        else if (rotation.y != 0)
        {
            currentRotation.y = rotation.y;
        }
        else if (rotation.z != 0)
        {
            currentRotation.z = rotation.z;
        }
        cinemachineCamera.transform.rotation = Quaternion.Euler(currentRotation);
    }
    public void ZoomOut()
    {
        if (zoomCollider != null)
        {
            zoomCollider.enabled = true;
        }
    }
    private void OnDestroy()
    {
        ZoomManager.Instance.UnregisterZoom();
        ZoomManager.Instance.currentZooms.Remove(this);
    }
}
