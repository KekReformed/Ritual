using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "CrowPassive", menuName = "Spells/Passive/Crow Passive")]
public class CrowPassive : PassiveSpell
{
    [SerializeField] int midairJumps;
    [SerializeField] float dashCooldown;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashLength;
    
    CrowPassiveMono _component;

    protected override bool Enable()
    {
        if(!base.Enable()) return false;
        _component = PlayerManager.Instance.AddComponent<CrowPassiveMono>();
        _component.Setup(midairJumps,dashCooldown,dashSpeed,dashLength);
        
        return true;
    }

    protected override void Disable()
    {
        base.Disable();
        Destroy(_component);
    }

}
