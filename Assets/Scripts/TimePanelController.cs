using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimePanelController : MonoBehaviour
{
    public int Value_1;
    public int Value_2;
    public int Value_3;
    public TimeSpan goalTime;
    public TMP_Text timerText;
    public GameObject timePanel;

    private DateTime startTime;
    private bool isGoalTimeReached = false;

    void Start()
    {
        // Set the time panel to be active by default
        timePanel.SetActive(true);

        // Set the start time to the current real time
        startTime = DateTime.Now;

        Value_1 = Javascripthooks.Instance.Value1;
        Value_2 = Javascripthooks.Instance.Value2;
        Value_3 = Javascripthooks.Instance.Value3;

        // Initialize goalTime using the Value1 variable
        goalTime = new TimeSpan(Value_1, Value_2, Value_3); // 5 minutes
    }


    void Update()
    {
        // Calculate how much time is left to reach the goal time
        TimeSpan timeLeft = goalTime - (DateTime.Now - startTime);

        // If the time left is negative, set it to zero
        if (timeLeft.TotalSeconds < 0)
        {
            timeLeft = new TimeSpan(0);
        }

        // Update the timer text to display the time left
        // timerText.text = timeLeft.ToString(@"hh\h\:mm\m\:ss\s");

        // timerText.text = string.Format("{0}hrs {1}mins {2}secs", (int)timeLeft.TotalHours, timeLeft.Minutes, timeLeft.Seconds);

        timerText.text = string.Format("<color=white>{0}</color><color=#FF69B4>hrs</color> <color=white>{1}</color><color=#FF69B4>mins</color> <color=white>{2}</color><color=#FF69B4>secs</color>", (int)timeLeft.TotalHours, timeLeft.Minutes, timeLeft.Seconds);
        timerText.richText = true;


        // If the goal time is reached, deactivate the time panel
        if (timeLeft.TotalSeconds <= 0.0f && !isGoalTimeReached)
        {
            timePanel.SetActive(false);
            isGoalTimeReached = true;
            // Add any additional code to enable game play here
        }
    }
}
