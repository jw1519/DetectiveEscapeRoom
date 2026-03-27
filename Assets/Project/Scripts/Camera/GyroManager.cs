using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class GyroManager : MonoBehaviour
{
    public static GyroManager Instance;
    UnityEngine.Gyroscope gyroscope;
    public Quaternion rotation;
    public bool isGyroActive;
    public bool enableGyro;

    public event Action onGyroEnable;
    public event Action onGyroDisable; 
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        isGyroActive = false;
        enableGyro = true;
        EnableGyro();
    }
    public void EnableGyro()
    {
        if (!enableGyro)
        {
            return;
        }
        if (SystemInfo.supportsGyroscope)
        {
            gyroscope = Input.gyro;
            gyroscope.enabled = true;
            isGyroActive = gyroscope.enabled;
            InputSystem.EnableDevice(Accelerometer.current);
            InputSystem.EnableDevice(AttitudeSensor.current);
            onGyroEnable?.Invoke();
        }
        else
        {
            isGyroActive = false;
        }
    }
    public void DisableGyro()
    {
        if (!isGyroActive) return;
        isGyroActive = false;
        onGyroDisable?.Invoke();
    }
    private void Update()
    {
        if (isGyroActive)
        {
            rotation = gyroscope.attitude;
            rotation = new Quaternion(0, gyroscope.attitude.y, 0, -gyroscope.attitude.w);
        }
    }
}
