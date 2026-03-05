public class LockedDraw : Draw, ILock
{
    public void unlock()
    {
        canOpen = true;
        Open();
    }
}
