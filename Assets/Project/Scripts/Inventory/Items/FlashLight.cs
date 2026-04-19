using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/FlashLight")]
public class FlashLight : Item
{
    RevealLightSource lightSource;
    private void OnEnable()
    {
        lightSource = FindFirstObjectByType<RevealLightSource>();
    }
    public override void SelectItem()
    {
        base.SelectItem();
        UseItem();
    }
    public override void UseItem()
    {
        if (lightSource == null)
        {
            lightSource = FindFirstObjectByType<RevealLightSource>();
        }
        lightSource.TurnLightOnOrOff();
    }
}
