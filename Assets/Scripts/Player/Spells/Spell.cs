using UnityEngine;

public abstract class Spell : ScriptableObject
{
    [SerializeField] public float cost;
    [Tooltip("Cooldown in seconds")] public float cooldown;
    public bool automatic;
    public Sprite icon;
    
    public virtual bool Use()
    {
        PlayerManager.Mana -= cost;
        return true;
    }
}
