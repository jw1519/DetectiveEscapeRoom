using System;
using UnityEngine;

public class ColourCombinationLockManager : MonoBehaviour
{
    public string[] correctCombination;
    public string[] currentCombination;

    public GameObject leftDoor;
    public GameObject rightDoor;
    private void OnEnable()
    {
        Rotation.Rotated += CheckCombination;
    }
    private void OnDisable()
    {
        Rotation.Rotated -= CheckCombination;
    }
    void Start()
    {
        Rotation.Rotated += CheckCombination;
        //set initial combination to all LightBlue as its the starting colour
        currentCombination = new string[correctCombination.Length];
        for (int i = 0; i < currentCombination.Length; i++)
        {
            currentCombination[i] = "LightBlue";
        }
    }
    public void CheckCombination(string wheelName, string colour)
    {
        switch (wheelName)
        {
            case "Wheel_1":
                currentCombination[0] = colour;
                break;
            case "Wheel_2":
                currentCombination[1] = colour;
                break;
            case "Wheel_3":
                currentCombination[2] = colour;
                break;
            case "Wheel_4":
                currentCombination[3] = colour;
                break;
        }
        if (IsCombinationCorrect())
        {
            Debug.Log("Combination Correct! Lock Opened.");
            Destroy(gameObject);
        }
    }
    public bool IsCombinationCorrect()
    {
        for (int i = 0; i < correctCombination.Length; i++)
        {
            if (currentCombination[i] != correctCombination[i])
            {
                return false;
            }
        }
        if (leftDoor !=null)
            leftDoor.GetComponent<ILock>().unlock();
        if (rightDoor !=null)
            rightDoor.GetComponent<ILock>().unlock();
        return true;
    }
}
