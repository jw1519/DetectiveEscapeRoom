using UnityEngine;

public class TrapDoor : CabinetDoor
{
    public GameObject carpet;
    public Vector3 move;

    public override void OnMouseDown()
    {
        if (carpet.transform.position != move)
        {
            carpet.transform.localPosition = move;
        }
        base.OnMouseDown();
    }
}
