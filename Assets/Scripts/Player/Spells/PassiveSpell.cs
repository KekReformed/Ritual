using UnityEngine;

public class PassiveSpell : Spell
{

    [HideInInspector] public bool enabled = false; 
        
    public override void Use()
    {
        if (enabled) Disable();
        else Enable();
    }

    public virtual void Enable()
    {
        if (PlayerManager.Mana < cost) return;
        
        PlayerManager.Mana -= cost;
        PlayerManager.MaxMana -= cost;
        enabled = true;
    }

    public virtual void Disable()
    {
        PlayerManager.Mana += cost;
        PlayerManager.MaxMana += cost;
        enabled = false;
    }
}
