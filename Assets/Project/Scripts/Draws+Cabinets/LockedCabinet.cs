

public class LockedCabinetDoor : CabinetDoor, ILock
{
    public void unlock()
    {
        canOpen = true;

        if (doorType == DoorType.right)
        {
            OpenRightDoor();
        }
        else
            OpenLeftDoor();
    }
}
