using UnityEngine;

public class MobileCameraControl : MonoBehaviour
{
    public int movementSpeed;
    private void FixedUpdate()
    {
        if (GyroManager.Instance.isGyroActive)
            transform.localRotation = GyroManager.Instance.rotation;
    }
}
