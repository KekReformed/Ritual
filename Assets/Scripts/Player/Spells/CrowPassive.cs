using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrowPassive : PassiveSpell
{
    private Rigidbody _rb;
    private InputAction _dashAction;
    private InputAction _moveAction;
    private InputAction _jumpAction;

    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashSpeed;

    private void Awake()
    {
        TimerManager.AddTimer(new Timer("DashCooldown",dashCooldown));
        
        float targetAngle = PlayerManager.GetAngleTowardsVectorFromCamera(Vector3.forward);
        _moveAction = PlayerManager.PlayerInput.actions.FindAction("Move");
        _dashAction = PlayerManager.PlayerInput.actions.FindAction("Dash");
        _jumpAction = PlayerManager.PlayerInput.actions.FindAction("Jump");
    }

    private void Dash()
    {
        if (!_dashAction.triggered || !TimerManager.CheckTimer("DashCooldown")) return;
        
        float targetAngle = PlayerManager.GetAngleTowardsVectorFromCamera(_moveAction.ReadValue<Vector2>());
        
        TimerManager.ResetTimer("DashCooldown");

        Vector3 move = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * dashSpeed;

        PlayerManager.Movement.SetMovementVector(new MovementVector(move, 1));
    }

    private void DoubleJump()
    {
        
    }
}
