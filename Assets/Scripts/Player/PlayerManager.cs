using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public static PlayerMovement Movement;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple player managers! there should only be 1!");
            return;
        }

        Instance = this;
        Movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        TimerManager.UpdateTimers();
    }
}
