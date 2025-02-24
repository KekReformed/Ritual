using UnityEngine;
using UnityEditor;

public class PassiveSpell : Spell
{

    [SerializeField] bool enabled;

    public virtual void Reset()
    {
        enabled = false;
    }
    
    public override bool Use()
    {
        if (enabled) Disable();
        else Enable();

        return true;
    }

    protected virtual bool Enable()
    {
        if (PlayerManager.Mana < cost) return false;
        
        PlayerManager.MaxMana -= cost;
        PlayerManager.Mana -= cost;
        enabled = true;

        return true;
    }

    protected virtual void Disable()
    {
        PlayerManager.MaxMana += cost;
        PlayerManager.Mana += cost;
        enabled = false;
    }
}
