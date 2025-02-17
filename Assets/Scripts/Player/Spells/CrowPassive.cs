using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "CrowPassive", menuName = "Spells/Passive/Crow Passive")]
public class CrowPassive : PassiveSpell
{
    [SerializeField] private int midairJumps;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashSpeed;
    
    private InputAction _dashAction;
    private InputAction _moveAction;
    private InputAction _jumpAction;

    private CrowPassiveMono _component;

    public override void Enable()
    {
        base.Enable();
        _component = PlayerManager.Instance.AddComponent<CrowPassiveMono>();
        _component.Setup(midairJumps,dashCooldown,dashSpeed);
    }

    public override void Disable()
    {
        base.Disable();
        Destroy(_component);
    }

}
