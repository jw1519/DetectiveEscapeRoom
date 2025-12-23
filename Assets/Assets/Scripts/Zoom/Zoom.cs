using UnityEngine;
using UnityEngine.EventSystems;

public class Zoom : MonoBehaviour
{
    Collider zoomCollider;
    public int zoomView;
    public Quaternion rotation;
    public Vector3 targetPosition;
    Camera cam => Camera.main;
    private void Awake()
    {
        zoomCollider = GetComponent<Collider>();
    }
    private void OnMouseDown()
    {
        // check for UI interaction
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        // check for touch input (mobile)
        //else if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        //    return;

        ZoomIn();
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
            cam.transform.position = targetPosition;
        }
        cam.transform.LookAt(transform, Vector3.up);
        ZoomManager.Instance.RegisterZoom(this);
        cam.fieldOfView = zoomView;
        // Apply rotation while maintaining other axes
        Vector3 currentRotation = cam.transform.rotation.eulerAngles;
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
        cam.transform.rotation = Quaternion.Euler(currentRotation);
    }
    public void ZoomOut()
    {
        if (zoomCollider != null)
        {
            zoomCollider.enabled = true;
        }
        Debug.Log("Zooming out");
    }
    private void OnDestroy()
    {
        ZoomManager.Instance.currentZooms.Remove(this);
    }
}
