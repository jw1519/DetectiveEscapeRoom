using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<ItemPoint> rooms;
    public bool isCompleted => rooms.TrueForAll(room => room.isComplete); // check is all room are complete


    void Update()
    {
        if (isCompleted)
        {
            Debug.Log("complete puzzle");
        }
    }
    public bool Check()
    {
        return false;
    }
}
