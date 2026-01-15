using System;
using UnityEngine;

public class ClockHands : MonoBehaviour
{
    public static event Action onHourHandMoved;
    public static event Action onMinuteHandMoved;
    public HandType handType;
    public enum HandType
    {
        Hour,
        Minute
    }
    private void OnMouseDown()
    {
        if (handType == HandType.Minute)
            onMinuteHandMoved?.Invoke();

        else if (handType == HandType.Hour)
            onHourHandMoved?.Invoke();

        Debug.Log(handType + " hand moved.");
    }
}
