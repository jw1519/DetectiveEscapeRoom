public class LockedCabinetDoor : CabinetDoor, ILock
{
    public void unlock()
    {
        canOpen = true;
        Open();
    }
}
