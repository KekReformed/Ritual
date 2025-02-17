using System.Collections.Generic;
using UnityEngine;

public class TimerManager
{
    public static Dictionary<string,Timer> Timers = new Dictionary<string, Timer>();

    /// <summary>
    /// Add a new timer to the timer manager
    /// </summary>
    public static void AddTimer(Timer timer)
    {
        if (Timers.ContainsKey(timer.Name)) return;
        Timers.Add(timer.Name,timer);
    }

    /// <summary>
    /// Checks if a timer has finished, returning true if it has false otherwise;
    /// </summary>
    /// <param name="TimerName">Name of the timer</param>
    public static bool CheckTimer(string TimerName)
    {
        return Timers[TimerName].CurrentTime <= 0f;
    }
    
    /// <summary>
    /// Resets a timer back to its MaxTime
    /// </summary>
    public static void ResetTimer(string timerName)
    {
        Timer timer = Timers[timerName];
        
        timer.CurrentTime = timer.MaxTime;
    }
    
    
    /// <summary>
    /// Used to update all timers, should only be ran once in the player manager or similiar area
    /// </summary>
    public static void UpdateTimers()
    {
        foreach (Timer timer in Timers.Values)
        {
            if (timer.CurrentTime < 0f) continue;
            
            timer.CurrentTime -= Time.deltaTime;
        }
    }
}
