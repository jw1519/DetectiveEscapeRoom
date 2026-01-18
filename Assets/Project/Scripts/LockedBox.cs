using UnityEngine;

public class LockedBox : MonoBehaviour, ILock
{
    public GameObject boxLid;
    public void unlock()
    {
        GetComponent<Collider>().enabled = false;
        boxLid.transform.Rotate(new Vector3(-90f, 0f, 0f));
    }
}
