using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public static PlayerMovement Movement;
    public static PlayerInput PlayerInput;
    public int Mana;

    public TMP_Text manaText;
    
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
        PlayerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        TimerManager.UpdateTimers();
    }

    public static void SetMana(int mana)
    {
        Instance.Mana = mana;
        Instance.manaText.SetText(mana.ToString());
    }
}
