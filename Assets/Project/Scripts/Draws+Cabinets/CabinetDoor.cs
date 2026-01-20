using System.Collections.Generic;
using UnityEngine;

public class CabinetDoor : MonoBehaviour
{
    public DoorType doorType;
    bool isopen = false;
    public bool canOpen;
    public List<GameObject> objectsInCabinet;
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
    private void OnMouseDown()
    {
        if (isopen) return;
        if (!canOpen) return;

        if (doorType == DoorType.right)
        {
            OpenRightDoor();
        }
        else
            OpenLeftDoor();
    }
    public void OpenRightDoor()
    {
        gameObject.transform.localRotation = Quaternion.Euler(0, -90, 0);
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
        gameObject.transform.localRotation = Quaternion.Euler(0, 90, 0);
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
        Debug.Log("Closing door");
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        isopen = false;
    }
    public enum DoorType
    {
        right, left,
    }
}
