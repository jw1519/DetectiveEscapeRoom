using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : BasePanel
{
    public GameObject gyro; 
    public bool gyroActive;

    public Slider rotationSpeedSlider;
    public override void Start()
    {
        base.Start();
         gyroActive = GyroManager.Instance.isGyroActive;
        if (!gyroActive)
        {
            gyro.SetActive(false);
        }
    }
    public void ToggleGyro()
    {
        if (gyroActive)
        {
            GyroManager.Instance.DisableGyro();
            gyroActive = false;
            GyroManager.Instance.enableGyro = false;
        }
        else
        {
            GyroManager.Instance.enableGyro = true;
            GyroManager.Instance.EnableGyro();
            gyroActive = true;
        }
    }
    public override void ClosePanel()
    {
        base.ClosePanel();
        ChangeRotationSpeed();
    }
    Inspect inspect;
    public void ChangeRotationSpeed()
    {
        if (inspect == null)
        {
            GameObject InspectCamera = Camera.main.gameObject.GetComponent<CameraController>().inspectCamera.gameObject;
            inspect = InspectCamera.GetComponent<Inspect>();
        }
        inspect.rotationSpeed = rotationSpeedSlider.value;
    }
}
