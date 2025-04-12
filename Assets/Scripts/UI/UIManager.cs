using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{

    ResourceBar _passiveManaBar;
    InputAction _radialPrimaryOpen;
    InputAction _radialSecondaryOpen;

    [SerializeField] SpellSlot primarySpellSlot;
    [SerializeField] SpellSlot secondarySpellSlot;
    [SerializeField] TMP_Text manaText;
    [SerializeField] TMP_Text manaRegenText;
    [SerializeField] TMP_Text healthText;
    
    public static SpellSlot PrimarySpellSlot;
    public static SpellSlot SecondarySpellSlot;
    public static TMP_Text ManaText;
    public static TMP_Text ManaRegenText;
    public static TMP_Text HealthText;
    public static RadialUI RadialUI;
    public static List<ResourceBar> ResourceBarList = new List<ResourceBar>();
    public static Dictionary<string, ResourceBar> ResourceBars = new Dictionary<string,ResourceBar>();
    public static UIManager Instance;
    
    void Awake()
    {
        PrimarySpellSlot = primarySpellSlot;
        SecondarySpellSlot = secondarySpellSlot;
        
        ManaText = manaText;
        ManaRegenText = manaRegenText;
        HealthText = healthText;
        
        RadialUI = GetComponentInChildren<RadialUI>();
        
        RadialUI.gameObject.SetActive(true);
        RadialUI.Setup();
        RadialUI.gameObject.SetActive(false);
        
        if (Instance != null)
        {
            Debug.LogError("Multiple UI managers! there should only be 1!");
            return;
        }
        Instance = this;
    }
    
    void Start()
    {
        foreach (ResourceBar bar in ResourceBarList)
        {
            ResourceBars[bar.resourceString] = bar;
        }
        
        _radialPrimaryOpen = PlayerManager.PlayerInput.actions.FindAction("SelectPrimarySpell");
        _radialSecondaryOpen = PlayerManager.PlayerInput.actions.FindAction("SelectSecondarySpell");
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (_radialPrimaryOpen.triggered || _radialSecondaryOpen.triggered)
        {
            RadialUI.gameObject.SetActive(true);
            RadialUI.Open();
        }

        if (_radialPrimaryOpen.WasReleasedThisFrame())
        {
            RadialUI.GetSpell();
            if (_radialSecondaryOpen.ReadValue<float>() == 0)
            {
                RadialUI.Close();
                RadialUI.gameObject.SetActive(false);
            }
        }
        
        if (_radialSecondaryOpen.WasReleasedThisFrame())
        {
            RadialUI.GetSpell(true);
            if (_radialPrimaryOpen.ReadValue<float>() == 0)
            {
                RadialUI.Close();
                RadialUI.gameObject.SetActive(false);
            }
        }
    }

    public static void SetManaUI(float mana, float maxMana, float passiveManaUsage)
    {
        ManaText.SetText($"{Mathf.Round(mana)}/{Mathf.Round(maxMana)}");
        
        ResourceBars["Mana"].SetScale(mana / (maxMana + passiveManaUsage) + passiveManaUsage / (maxMana + passiveManaUsage));
        ResourceBars["PassiveMana"].UpdateResource(passiveManaUsage, maxMana + passiveManaUsage);
    }
}
