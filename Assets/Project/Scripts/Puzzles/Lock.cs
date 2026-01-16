using UnityEngine;

public class Lock : MonoBehaviour, ILock
{
    public Item KeyItem;

    public bool IsLocked = false;

    public void unlock()
    {
        IsLocked = false;
    }
    pub
}
