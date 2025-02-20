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
    
    static float _mana;
    static float _maxMana;

    public static float Mana
    {
        get => _mana;
        set
        {
            _mana = value;

            Instance.manaText.SetText($"{_mana}/{_maxMana}");
        }
    }

    public static float MaxMana
    {
        get => _maxMana;
        set
        {
            _maxMana = value;
            Instance.manaText.SetText($"{_mana}/{_maxMana}");
        }
    }

    public TMP_Text manaText;
    
    [SerializeField] float _startingMana;
    [SerializeField] float _startingMaxMana;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple player managers! there should only be 1!");
            return;
        }
        Instance = this;

        Mana = _startingMana;
        MaxMana = _startingMaxMana;
        Movement = GetComponent<PlayerMovement>();
        PlayerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        TimerManager.UpdateTimers();
    }
    
    /// <summary>
    /// Gets the angle between the characters forward direction, and the way the camera is currently facing useful for correcting movement towards camera
    /// </summary>
    public static float GetAngleTowardsVectorFromCamera(Vector2 targetVector)
    {
        float targetAngle = Mathf.Atan2(targetVector.x, targetVector.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        return targetAngle;
    }
}
