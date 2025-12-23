using UnityEngine;

public class CabinetDoor : MonoBehaviour
{
    public DoorType doorType;
    public Transform leftDoor;
    public Transform rightDoor;
    bool isopen = false;
    public bool canOpen;
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
        rightDoor.localRotation = Quaternion.Euler(0, -90, 0);
        isopen = true;
    }
    public void OpenLeftDoor()
    {
        leftDoor.localRotation = Quaternion.Euler(0, 90, 0);
        isopen = true;
    }
    public enum DoorType
    {
        right, left,
    }
    public void unlock(Item item)
    {

    }
}
