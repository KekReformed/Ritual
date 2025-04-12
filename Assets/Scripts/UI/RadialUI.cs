using System;
using TMPro;
using UnityEngine;

public class RadialUI : MonoBehaviour
{
    //Angle Offset
    const float Offset = -90;
    
    [SerializeField] Spell[] spellList = new Spell[8];
    public static Spell[] SpellList = new Spell[8];
    
    public void Setup()
    {
        SpellList = spellList;
        for (int i = 0; i < SpellList.Length; i++)
        {
            Spell spell = spellList[i];
            if(spell == null) continue;

            Debug.Log($"Pizza Slice {i+1}");
            
            GameObject PizzaSlice = GameObject.Find($"Pizza Slice {i+1}");

            PizzaSlice.GetComponentInChildren<TMP_Text>().SetText(spell.name);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        GetArc();
    }

    int GetArc()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 direction = mousePosition - new Vector2(Screen.width / 2, Screen.height / 2);
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + Offset;
        if (angle < 0) angle += 360; // Convert negative angles to positive range (0 to 360)

        int arcIndex = 7 - Mathf.FloorToInt(angle / 45); // 360° / 8 arcs = 45° per arc

        return arcIndex;
    }

    public void GetSpell(bool altSpell = false)
    {
        int index = GetArc();
        
        if(spellList[index] == null) return;
        
        if (!altSpell)
        {
            PlayerManager.PlayerSpellcasting.MainSpell = SpellList[index];
            UIManager.PrimarySpellSlot.SetSpellSlot(SpellList[index].name);
        }
        else
        {
            PlayerManager.PlayerSpellcasting.AltSpell = SpellList[index];
            UIManager.SecondarySpellSlot.SetSpellSlot(SpellList[index].name);
        }
    }

    public void Open()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    
    public void Close()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
