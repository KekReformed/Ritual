public class ActiveSpell : Spell
{
    public override void Use()
    {
        if(PlayerManager.Mana < cost) return;
        PlayerManager.Mana -= cost;
    }
}

