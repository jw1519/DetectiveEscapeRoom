using UnityEngine;

public class SettingsPanel : BasePanel
{

    public bool gyroActive;
    public override void Start()
    {
        base.Start();
         gyroActive = GyroManager.Instance.isGyroActive;
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
}
