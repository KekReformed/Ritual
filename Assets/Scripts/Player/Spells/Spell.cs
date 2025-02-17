using UnityEngine;

public abstract class Spell : ScriptableObject
{
    [SerializeField] public float cost;
    [Tooltip("Cooldown in seconds")] public float cooldown;

    //Returns a bool corresponding if we can cast the spell or not, use later for things like stuns
    public virtual void Use()
    {
        PlayerManager.SetMana(PlayerManager.Mana - cost);
    }
}
