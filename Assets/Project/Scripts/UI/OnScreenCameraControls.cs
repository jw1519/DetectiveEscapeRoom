using UnityEngine;
using UnityEngine.UI;

public class OnScreenCameraControls : BasePanel
{
    public Button left;
    public Button right;

    public void SetControls(CameraController cam)
    {
        left.onClick.AddListener(() => cam.TurnLeft());
        right.onClick.AddListener(() => cam.TurnRight());
    }
}
