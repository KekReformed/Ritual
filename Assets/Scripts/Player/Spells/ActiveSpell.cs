public class ActiveSpell : Spell
{
    public override bool Use()
    {
        if(PlayerManager.Mana < cost) return false;
        PlayerManager.Mana -= cost;
        return true;
    }
}

