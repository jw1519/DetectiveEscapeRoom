using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public int targetHour;
    public int targetMinute;

    public Transform clockFace;
    public GameObject item;

    int angle = -30;
    int currentHour = 0;
    int currentMinute = 0;

    public void Awake()
    {
        // Initialize current time based on the initial rotation of the hands
        currentHour = Mathf.RoundToInt(-hourHand.localEulerAngles.z / 30) % 12;
        currentMinute = Mathf.RoundToInt(-minuteHand.localEulerAngles.z / 6) % 60;
    }

    private void OnEnable()
    {
        ClockHands.onHourHandMoved += MoveHourhand;
        ClockHands.onMinuteHandMoved += MoveMinutehand;
    }
    private void OnDisable()
    {
        ClockHands.onHourHandMoved -= MoveHourhand;
        ClockHands.onMinuteHandMoved -= MoveMinutehand;
    }

    public void MoveHourhand()
    {
        hourHand.localEulerAngles = new Vector3(0, 0, hourHand.localEulerAngles.z + angle);
        currentHour = (currentHour + 1) % 12; // Update current hour but wrap around after 12
        if (IsPuzzleSolved())
        {
            Solved();
        }
    }
    public void MoveMinutehand()
    {
        minuteHand.localEulerAngles = new Vector3(0, 0, minuteHand.localEulerAngles.z + angle);
        currentMinute = (currentMinute + 5) % 60; // Update current minute but wrap around after 60
        if (IsPuzzleSolved())
        {
            Solved();
        }
    }
    public bool IsPuzzleSolved()
    {
        return currentHour == targetHour && currentMinute == targetMinute;
    }
    public void Solved()
    {
        item.GetComponent<Collider>().enabled = true;
        clockFace.localRotation = Quaternion.Euler(0, -90, 0);
    }

}
