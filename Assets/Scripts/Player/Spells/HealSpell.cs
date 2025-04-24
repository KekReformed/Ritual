using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Spells/Active/Heal")]
public class HealSpell : ActiveSpell
{
    [SerializeField] float healAmount;
    public override bool Use()
    {
        if (!base.Use()) return false;
        
        PlayerManager.Instance.Damage(-healAmount);

        return true;
    }
}
