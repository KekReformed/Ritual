using UnityEngine;

public abstract class Spell : ScriptableObject
{
    [SerializeField] public float cost;
    [Tooltip("Cooldown in seconds")] public float cooldown;
    
    public virtual bool Use()
    {
        PlayerManager.Mana -= cost;
        return true;
    }
}
