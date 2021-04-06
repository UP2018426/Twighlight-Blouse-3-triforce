using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace common
{
    public class Timer 
    {
        public static bool Countdown(float timeToCount)
        {
            bool timerFinished = false;
            timeToCount -= Time.deltaTime;
            timeToCount = Mathf.Clamp(timeToCount, 0f, Mathf.Infinity);
            if(timeToCount <= 0)
            {
                return timerFinished;
            }
            else
            {
                return timerFinished;
            }
        }
    }
}