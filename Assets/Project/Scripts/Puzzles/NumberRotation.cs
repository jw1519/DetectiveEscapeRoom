using System;
using System.Collections.Generic;
using UnityEngine;

public class NumberRotation : MonoBehaviour
{
    public static event Action<string, int> Rotated;
    List<int> numbers;
    public int currentNumber = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    }

    public void Rotate()
    {
        transform.RotateAround(transform.position, Vector3.right, 36);
        currentNumber = numbers[(numbers.IndexOf(currentNumber) + 1) % numbers.Count];
        Rotated?.Invoke(gameObject.name, currentNumber);
    }
    private void OnMouseDown()
    {
        Rotate();
    }
}
