using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 moveLocation;
    private void OnMouseDown()
    {
        transform.localPosition = moveLocation;
    }
}
