using UnityEngine;

public class NumberLock : MonoBehaviour
{
    public int[] correctCombination;
    public int[] currentCombination;
    public GameObject leftDoor;
    public GameObject rightDoor;
    private void OnEnable()
    {
        NumberRotation.Rotated += CheckCombination;
    }
    private void OnDisable()
    {
        NumberRotation.Rotated -= CheckCombination;
    }
    void Start()
    {
        NumberRotation.Rotated += CheckCombination;
        //set initial combination 
        currentCombination = new int[correctCombination.Length];
        for (int i = 0; i < currentCombination.Length; i++)
        {
            currentCombination[i] = 0;
        }
    }
    public void CheckCombination(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "Ruller1":
                currentCombination[0] = number;
                break;
            case "Ruller2":
                currentCombination[1] = number;
                break;
            case "Ruller3":
                currentCombination[2] = number;
                break;
            case "Ruller4":
                currentCombination[3] = number;
                break;
        }
        if (IsCombinationCorrect())
        {
            ZoomManager.Instance.UnregisterZoom();
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
        if (leftDoor != null)
            leftDoor.GetComponent<ILock>().unlock();
        if (rightDoor != null)
            rightDoor.GetComponent<ILock>().unlock();
        return true;
    }
}
