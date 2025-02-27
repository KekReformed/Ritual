using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour, IDamageable
{
    public static PlayerManager Instance;
    public static PlayerMovement Movement;
    public static PlayerInput PlayerInput;
    
    static float _mana;
    static float _maxMana;

    static float _health;
    static float _maxHealth;

    public static float Mana
    {
        get => _mana;
        set
        {
            _mana = value;

            SetManaText();
        }
    }

    
    public static float MaxMana
    {
        get => _maxMana;
        set
        {
            _maxMana = value;
            SetManaText();
        }
    }

    static void SetManaText()
    {
        Instance.manaText.SetText($"{Mathf.Round(_mana)}/{Mathf.Round(_maxMana)}");
    }

    public TMP_Text manaText;
    public TMP_Text healthText;
    
    [SerializeField] float startingMana;
    [SerializeField] float startingMaxMana;
    
    [SerializeField] float startingHp;
    [SerializeField] float startingMaxHp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple player managers! there should only be 1!");
            return;
        }
        Instance = this;

        Mana = startingMana;
        MaxMana = startingMaxMana;
        
        _health = startingHp;
        _maxHealth = startingMaxHp;
        
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
    
    public void Damage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
        }
        Instance.healthText.SetText($"{Mathf.Max(_health,0)}/{Mathf.Round(_maxHealth)}");
    }
}
