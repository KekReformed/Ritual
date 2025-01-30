using System.Collections.Generic;
using UnityEngine;

public class TimerManager
{
    public static Dictionary<string,Timer> Timers = new Dictionary<string, Timer>();

    public static void AddTimer(Timer timer)
    { ;
        Timers.Add(timer.Name,timer);
    }

    public static void ResetTimer(string timerName)
    {
        Timer timer = Timers[timerName];
        timer.CurrentTime = timer.MaxTime;
    }
    
    public static void UpdateTimers()
    {
        foreach (Timer timer in Timers.Values)
        {
            if (timer.CurrentTime < 0f) continue;
            
            timer.CurrentTime -= Time.deltaTime;
        }
    }
}
