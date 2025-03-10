using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    private Dictionary<string, Dictionary<string, int>> _stats = new Dictionary<string, Dictionary<string, int>>();


    public int GetStat(string key, string secondaryKey)
    {
        InitialiseStatIfUninitialised(key, secondaryKey);

        return _stats[key][secondaryKey];
    }
    
    public void SetStat(string key, string secondaryKey, int value)
    {
        InitialiseStatIfUninitialised(key, secondaryKey);
        _stats[key][secondaryKey] = value;
    }
    
    public void InitialiseStatIfUninitialised(string key, string secondaryKey)
    {
        if (!_stats.ContainsKey(key))
        {
            _stats.Add(key, new Dictionary<string, int> {{secondaryKey,0}});
        }
    }

    public void IncrementStat(string key, string secondaryKey, int value = 1)
    {
        InitialiseStatIfUninitialised(key,secondaryKey);

        _stats[key][secondaryKey] += value;
    }
}
