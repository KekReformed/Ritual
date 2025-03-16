using System;
using TMPro;
using UnityEngine;

public class SpellSlot : MonoBehaviour
{
    TMP_Text _text;

    void Awake()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }
    
    public void SetSpellSlot(string spellName)
    {
        _text.SetText(spellName);
    }
}
