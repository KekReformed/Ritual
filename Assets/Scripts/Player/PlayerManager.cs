using System;
using System.Collections;
using System.Collections.Generic;
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
    public static PlayerSpellcasting PlayerSpellcasting;
    public static float PassiveManaUsage;
    public static PlayerStats PlayerStats = new PlayerStats();
    
    static float _mana;
    static float _maxMana;

    public static float Health { get; set; }
    public static float MaxHealth { get; private set; }

    public static float Mana
    {
        get => _mana;
        set
        {
            _mana = value;

            UIManager.SetManaUI(_mana,_maxMana,PassiveManaUsage);
        }
    }
    
    public static float MaxMana
    {
        get => _maxMana;
        set
        {
            if (_maxMana >= value) PassiveManaUsage = Mathf.Abs(value - _maxMana);
            else PassiveManaUsage -= (value - _maxMana);

            _maxMana = value;
            UIManager.SetManaUI(_mana,_maxMana,PassiveManaUsage);
        }
    }

    public int skillPoints;
    
    [SerializeField] float startingMana;
    [SerializeField] float startingMaxMana;
    
    [SerializeField] float startingHp;
    [SerializeField] float startingMaxHp;
    [SerializeField] [Range(0.01f,0.2f)] float manaRegenPercent;

    public HashSet<string> Upgrades = new HashSet<string>();
    public HashSet<string> Inventory = new HashSet<string>();
    public bool completedAQuest;

    [SerializeField] CrowPassive crowPassive;
    
    void Awake()
    {
        Movement = GetComponent<PlayerMovement>();
        PlayerInput = GetComponent<PlayerInput>();
        PlayerSpellcasting = GetComponent<PlayerSpellcasting>();
        
        if (Instance != null)
        {
            Debug.LogError("Multiple player managers! there should only be 1!");
            return;
        }
        Instance = this;
    }

    void Start()
    {
        crowPassive.PublicDisable();
        Mana = startingMana;
        MaxMana = startingMaxMana;
        PassiveManaUsage = 0;
        
        Health = startingHp;
        MaxHealth = startingMaxHp;
        Upgrades.Clear();
        Inventory.Clear();
    }

    void Update()
    {
        TimerManager.UpdateTimers();
        float manaRegen = _maxMana * manaRegenPercent;
        
        UIManager.ManaRegenText.SetText($"+{Mathf.Round(manaRegen * 100) / 100}/s");
        Mana = Mathf.Min(_maxMana, _mana + manaRegen * Time.deltaTime);
        
        if(Input.GetKeyDown(KeyCode.M)) Damage(200);
    }
    
    /// <summary>
    /// Gets the angle between the characters forward direction, and the way the camera is currently facing useful for correcting movement towards camera
    /// </summary>
    public static float GetAngleTowardsVectorFromCamera(Vector2 targetVector)
    {
        if (Camera.main == null) return Mathf.Atan2(targetVector.x, targetVector.y) * Mathf.Rad2Deg;
        
        float targetAngle = Mathf.Atan2(targetVector.x, targetVector.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        return targetAngle;
    }
    
    public void Damage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
        }
        Health = Mathf.Clamp(Health, -10, MaxHealth);
        UIManager.HealthText.SetText($"{Mathf.Max(Health,0)}/{Mathf.Round(MaxHealth)}");
        UIManager.ResourceBars["Health"].UpdateResource(Health, MaxHealth);
    }
}
