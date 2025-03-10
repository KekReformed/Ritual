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
    public static float PassiveManaUsage;
    
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

            SetManaUI();
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
            SetManaUI();
        }
    }

    static void SetManaUI()
    {
        Instance.manaText.SetText($"{Mathf.Round(_mana)}/{Mathf.Round(_maxMana)}");
        
        UIManager.ResourceBars["Mana"].UpdateResource(Mana,UIManager.ResourceBars["PassiveMana"].rect.rect.width * UIManager.ResourceBars["PassiveMana"].scale);
        UIManager.ResourceBars["PassiveMana"].UpdateResource(PassiveManaUsage);
    }

    public TMP_Text manaText;
    public TMP_Text healthText;
    
    [SerializeField] float startingMana;
    [SerializeField] float startingMaxMana;
    
    [SerializeField] float startingHp;
    [SerializeField] float startingMaxHp;
    [SerializeField] [Range(0.01f,0.2f)] float manaRegenPercent;
    
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
        PassiveManaUsage = 0;
        
        _health = startingHp;
        _maxHealth = startingMaxHp;
        
        Movement = GetComponent<PlayerMovement>();
        PlayerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        TimerManager.UpdateTimers();
        Mana = Mathf.Min(_maxMana, _mana + _maxMana * manaRegenPercent * Time.deltaTime);
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
        UIManager.ResourceBars["Health"].UpdateResource(_health);
    }
}
