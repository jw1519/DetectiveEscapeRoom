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
    private void Update()
    {
        // check for UI interaction
        // check for touch input (mobile)
        Ray ray;
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount == 0) return;

            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;
            ray = cam.ScreenPointToRay(touch.position);
        }
        else
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;
            ray = cam.ScreenPointToRay(Input.mousePosition);
        }
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider == zoomCollider)
            {
                ZoomIn();
            }
        }
    }
    //private void OnMouseDown()
    //{
    //    // check for UI interaction
    //    // check for touch input (mobile)
    //    if (Application.isMobilePlatform)
    //    {
    //        if (Input.touchCount == 0) return;

    //        Touch touch = Input.GetTouch(0);
    //        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
    //            return;
    //    }
    //    else
    //    {
    //        if (!Input.GetMouseButtonDown(0)) return;
    //        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
    //            return;
    //    }
    //    ZoomIn();
    //}
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
