using System;
using UnityEngine;

public class KeyPad : MonoBehaviour
{
    public static event Action<string> inputChanged;
    public static string correctCode = "1562";
    public static string playerInput = "";
    // Update is called once per frame
    public void OnMouseDown()
    {
        if (playerInput.Length != correctCode.Length)
        {
            playerInput += gameObject.name;
            inputChanged?.Invoke(playerInput);
            Check();
        }
    }
    public void Check()
    {
        if (playerInput.Length == correctCode.Length)
        {
            if (playerInput == correctCode)
            {
                //unlock
                inputChanged?.Invoke("Correct");
                this.enabled = false;
            }
            else
            {
                inputChanged?.Invoke("Incorrect");
                playerInput = "";
            }
        }
    }
}
