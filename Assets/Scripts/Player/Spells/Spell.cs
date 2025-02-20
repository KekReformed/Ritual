using UnityEngine;

public abstract class Spell : ScriptableObject
{
    [SerializeField] public float cost;
    [Tooltip("Cooldown in seconds")] public float cooldown;
    
    public virtual void Use()
    {
        PlayerManager.Mana -= cost;
    }
}
