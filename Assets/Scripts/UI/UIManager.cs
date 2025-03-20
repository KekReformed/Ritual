using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static List<ResourceBar> ResourceBarList = new List<ResourceBar>();
    public static Dictionary<string, ResourceBar> ResourceBars = new Dictionary<string,ResourceBar>();
    public static UIManager Instance;
    ResourceBar _passiveManaBar;

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
    
    void Awake()
    {
        PrimarySpellSlot = primarySpellSlot;
        SecondarySpellSlot = secondarySpellSlot;
        ManaText = manaText;
        ManaRegenText = manaRegenText;
        HealthText = healthText;
        
        if (Instance != null)
        {
            Debug.LogError("Multiple player managers! there should only be 1!");
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
    }
    
    public static void SetManaUI(float mana, float maxMana, float passiveManaUsage)
    {
        ManaText.SetText($"{Mathf.Round(mana)}/{Mathf.Round(maxMana)}");
        
        ResourceBars["Mana"].SetScale(mana / (maxMana + passiveManaUsage) + passiveManaUsage / (maxMana + passiveManaUsage));
        ResourceBars["PassiveMana"].UpdateResource(passiveManaUsage, maxMana + passiveManaUsage);
    }
}
