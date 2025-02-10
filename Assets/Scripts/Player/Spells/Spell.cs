using System;

[Serializable]
public abstract class Spell
{
    protected float cost;
    public string name;
    public float cooldown;

    public virtual void Use()
    {
        PlayerManager.SetMana(PlayerManager.Instance.Mana - cost);
    }
}
