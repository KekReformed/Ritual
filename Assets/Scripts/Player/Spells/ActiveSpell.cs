using UnityEngine;

[CreateAssetMenu(fileName = "NothingSpell", menuName = "Spells/Active/None")]
public class ActiveSpell : Spell
{
    public override bool Use()
    {
        if(PlayerManager.Mana < cost) return false;
        PlayerManager.Mana -= cost;
        return true;
    }
}

