using UnityEngine;
public class RevealLightSource : MonoBehaviour
{
    public Light lightSource;
    float originalSpotAngle = 60f;
    bool isOn = false;
    private void Awake()
    {
        lightSource = GetComponent<Light>();
        lightSource.color = Color.magenta;
        if (isOn)
        {
            TurnLightOnOrOff();
        }
    }
    public void TurnLightOnOrOff()
    {
        if (!isOn)
        {
            lightSource.spotAngle = originalSpotAngle;
            lightSource.enabled = true;
            isOn = true;
        }
        else
        {
            lightSource.spotAngle = 0;
            lightSource.enabled = false;
            isOn = false;
        }
    }
}
