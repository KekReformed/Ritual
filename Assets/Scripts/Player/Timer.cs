using UnityEngine;

public class Timer
{
    public readonly string Name;
    public readonly float MaxTime;
    public float CurrentTime;
    
    public Timer(string name, float maxTime)
    {
        MaxTime = maxTime;
        Name = name;
    }
}
