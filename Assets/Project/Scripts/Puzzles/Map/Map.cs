using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<ItemPoint> rooms;
    public GameObject lockedBox;
    public ILock toUnlock;
    public bool isCompleted => rooms.TrueForAll(room => room.isComplete); // check is all room are complete
    private void Start()
    {
        toUnlock = lockedBox.GetComponent<ILock>();
    }

    public void Check()
    {
        if (isCompleted)
        {
            toUnlock?.unlock();
        }
        Debug.Log(isCompleted);
    }
}
