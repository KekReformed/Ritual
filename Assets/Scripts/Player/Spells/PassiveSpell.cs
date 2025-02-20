using UnityEngine;

public class PassiveSpell : Spell
{

    [HideInInspector] public bool enabled; 
        
    public override void Use()
    {
        if (enabled) Disable();
        else Enable();
    }

    public virtual void Enable()
    {
        if (PlayerManager.Mana < cost) return;
        
        PlayerManager.MaxMana -= cost;
        PlayerManager.Mana -= cost;
        enabled = true;
    }

    public virtual void Disable()
    {
        PlayerManager.MaxMana += cost;
        PlayerManager.Mana += cost;
        enabled = false;
    }
}
