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
    public static float Mana;
    public static float MaxMana;

    [SerializeField] float startingMana;
    [SerializeField] private float maxMana;
    
    public TMP_Text manaText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple player managers! there should only be 1!");
            return;
        }

        Mana = startingMana;
        MaxMana = maxMana;
        Instance = this;
        Movement = GetComponent<PlayerMovement>();
        PlayerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        TimerManager.UpdateTimers();
        MaxMana = Mathf.Min(Mana, MaxMana);
    }

    public static void SetMana(float mana)
    {
        Mana = mana;
        Instance.manaText.SetText(mana.ToString());
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
