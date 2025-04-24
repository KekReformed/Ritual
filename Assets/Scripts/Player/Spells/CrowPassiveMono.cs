using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CrowPassiveMono : MonoBehaviour
{
    Rigidbody _rb;
    InputAction _dashAction;
    InputAction _moveAction;
    InputAction _jumpAction;
    int _jumps;
    int _dashs;
    int _maxDashs;
    bool _dashing;
    MovementVector _movementVector;

    [SerializeField] int maxMidairJumps;
    [SerializeField] float dashSpeed;

    public void Setup(int maxMidairJumps, int maxMidairDashes, float dashCooldown, float dashSpeed, float dashLength)
    {
        TimerManager.AddTimer(new Timer("DashCooldown", dashCooldown + dashLength));
        TimerManager.AddTimer(new Timer("DashLength", dashLength));
        
        this.maxMidairJumps = maxMidairJumps;
        this.dashSpeed = dashSpeed;
        _dashs = maxMidairDashes;
        _maxDashs = maxMidairDashes;

        _moveAction = PlayerManager.PlayerInput.actions.FindAction("Move");
        _dashAction = PlayerManager.PlayerInput.actions.FindAction("Dash");
        _jumpAction = PlayerManager.PlayerInput.actions.FindAction("Jump");
    }

    void Update()
    {
        if (_dashAction.triggered && PlayerManager.Instance.Upgrades.Contains("Dash")) Dash();
        if (_jumpAction.triggered) DoubleJump();
        if (PlayerManager.Movement.grounded)
        {
            _jumps = maxMidairJumps;
            _dashs = _maxDashs;
        }
        
        if (_dashing)
        {
            PlayerManager.Movement.SetMovementVector(_movementVector);

            if (TimerManager.CheckTimer("DashLength"))
            {
                _dashing = false;
            }
        }
    }

    void Dash()
    {
        if (!TimerManager.CheckTimer("DashCooldown") || _dashs == 0) return;

        float targetAngle = PlayerManager.GetAngleTowardsVectorFromCamera(_moveAction.ReadValue<Vector2>());

        TimerManager.ResetTimer("DashCooldown");
        TimerManager.ResetTimer("DashLength");

        Vector3 move = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * dashSpeed;

        _movementVector =  new MovementVector(move, 1, false);
        _dashing = true;
        _dashs -= 1;
    }

    void DoubleJump()
    {
        if (PlayerManager.Movement.grounded || _jumps < 1) return;
        _jumps--;
        PlayerManager.Movement.Jump();
    }
}
