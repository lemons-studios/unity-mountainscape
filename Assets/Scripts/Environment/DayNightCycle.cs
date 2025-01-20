using System;
using System.Collections;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] 
    [Tooltip("In Seconds")] private int fullDayLength = 600;
    [SerializeField] private Light sun;
    
    private float rotationAmount;
    private void Start()
    {
        rotationAmount = 360 / (float) fullDayLength;
    }

    private void Update()
    {
        sun.gameObject.transform.Rotate(rotationAmount * Time.deltaTime, 0, 0, Space.Self);
    }
}
