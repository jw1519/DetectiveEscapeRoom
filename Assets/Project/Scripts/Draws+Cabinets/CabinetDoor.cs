using System.Collections.Generic;
using UnityEngine;

public class CabinetDoor : MonoBehaviour
{
    public DoorType doorType;
    bool isopen = false;
    public bool canOpen;
    public List<GameObject> objectsInCabinet;
    public rotationAxis axis;
    Vector3 closedRotation;
    private void Start()
    {
        ZoomManager.Instance.onZoomOut += CloseDoor;
    }
    void OnEnable()
    {
        if (ZoomManager.Instance != null)
        ZoomManager.Instance.onZoomOut += CloseDoor;
    }

    void OnDisable()
    {
        ZoomManager.Instance.onZoomOut -= CloseDoor;
    }
    private void OnDestroy()
    {
        ZoomManager.Instance.onZoomOut -= CloseDoor;
    }
    public virtual void OnMouseDown()
    {
        if (isopen) return;
        if (!canOpen) return;
        if (ManagerUI.IsTouchOverUI(Input.mousePosition)) return;

        if (doorType == DoorType.right)
        {
            OpenRightDoor();
        }
        else
            OpenLeftDoor();
    }
    public void OpenRightDoor()
    {
        closedRotation = transform.localRotation.eulerAngles;
        switch (axis)
        {
            case rotationAxis.x:
                transform.localRotation = Quaternion.Euler(-90, transform.localRotation.y, transform.localRotation.z);
                break;
            case rotationAxis.y:
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, -90, transform.localRotation.z);
                break;
            case rotationAxis.z:
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, -90);
                break;
        }
        isopen = true;
        if (objectsInCabinet == null) return;
        foreach (GameObject child in objectsInCabinet)
        {
            if (child.GetComponent<Collider>())
            {
                child.GetComponent<Collider>().enabled = true;
            }
        }
    }
    public void OpenLeftDoor()
    {
        closedRotation = transform.localRotation.eulerAngles;
        switch (axis)
        {
            case rotationAxis.x:
                transform.localRotation = Quaternion.Euler(90, transform.localRotation.y, transform.localRotation.z);
                break;
            case rotationAxis.y:
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, 90, transform.localRotation.z);
                break;
            case rotationAxis.z:
                transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 90);
                break;
        }
        isopen = true;
        if (objectsInCabinet == null) return;
        foreach (GameObject child in objectsInCabinet)
        {
            if (child.GetComponent<Collider>())
            {
                child.GetComponent<Collider>().enabled = true;
            }
        }
    }
    public void CloseDoor()
    {
        transform.localRotation = Quaternion.Euler(closedRotation);
        isopen = false;
    }
    public enum DoorType
    {
        right, left,
    }
    public enum rotationAxis
    {
        x, y, z,
    }
}
