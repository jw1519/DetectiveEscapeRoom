using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<ItemPoint> rooms;
    public ILock toUnlock;
    public bool isCompleted => rooms.TrueForAll(room => room.isComplete); // check is all room are complete

    public void Check()
    {
        if (isCompleted)
        {
            toUnlock?.unlock();
        }
    }
}
