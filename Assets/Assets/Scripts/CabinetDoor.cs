using UnityEngine;

public class CabinetDoor : MonoBehaviour
{
    public DoorType doorType;
    bool isopen = false;
    public bool canOpen;
    private void Start()
    {
        ZoomManager.Instance.onZoomOut += CloseDoor;
    }
    void OnEnable()
    {
        ZoomManager.Instance.onZoomOut += CloseDoor;
    }

    void OnDisable()
    {
        ZoomManager.Instance.onZoomOut -= CloseDoor;
    }
    private void OnMouseDown()
    {
        if (isopen) return;
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
    }
    public void OpenLeftDoor()
    {
        gameObject.transform.localRotation = Quaternion.Euler(0, 90, 0);
        isopen = true;
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
    public void unlock(Item item)
    {

    }
}
