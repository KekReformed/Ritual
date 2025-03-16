using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static List<ResourceBar> ResourceBarList = new List<ResourceBar>();
    public static Dictionary<string, ResourceBar> ResourceBars = new Dictionary<string,ResourceBar>();
    ResourceBar _passiveManaBar;

    [SerializeField] SpellSlot primarySpellSlot;
    [SerializeField] SpellSlot secondarySpellSlot;
    
    public static SpellSlot PrimarySpellSlot;
    public static SpellSlot SecondarySpellSlot;

    
    void Awake()
    {
        PrimarySpellSlot = primarySpellSlot;
        SecondarySpellSlot = secondarySpellSlot;
    }
    void Start()
    {
        foreach (ResourceBar bar in ResourceBarList)
        {
            ResourceBars[bar.resourceString] = bar;
        }
    }
}
