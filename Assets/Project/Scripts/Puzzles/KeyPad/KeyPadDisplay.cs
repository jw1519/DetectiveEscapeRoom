using TMPro;
using UnityEngine;
public class KeyPadDisplay : MonoBehaviour, ILock
{
    public TextMeshPro text;
    private void OnEnable()
    {
        KeyPad.inputChanged += UpdateDisplay;
    }
    private void OnDisable()
    {
        KeyPad.inputChanged -= UpdateDisplay;
    }
    public void UpdateDisplay(string playerInput)
    {
        text.text = playerInput;
        if (playerInput == "Correct")
        {
            text.text = "Unlocked";
            unlock();
        }
    }

    public void unlock()
    {
        ManagerUI.Instance.OpenPanel("WinningPanel");
    }
}