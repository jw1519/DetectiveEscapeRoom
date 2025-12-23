using TMPro;
using UnityEngine;
public class KeyPadDisplay : MonoBehaviour
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
    }
}