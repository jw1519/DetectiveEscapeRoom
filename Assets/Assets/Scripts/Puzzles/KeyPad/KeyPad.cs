using System;
using UnityEngine;

public class KeyPad : MonoBehaviour
{
    public static event Action<string> inputChanged;
    public static string correctCode = "1234";
    public static string playerInput = "";
    // Update is called once per frame
    void Update()
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
    private void OnMouseDown()
    {
        if (playerInput.Length != correctCode.Length)
        {
            playerInput += gameObject.name;
            inputChanged?.Invoke(playerInput);
        }
    }
}
