using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CrowPassiveMono : MonoBehaviour
{
    private Rigidbody _rb;
    private InputAction _dashAction;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private int _jumps;
    
    [SerializeField] int maxMidairJumps;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashSpeed;

    public void Setup(int maxMidairJumps, float dashCooldown, float dashSpeed)
    {
        TimerManager.AddTimer(new Timer("DashCooldown",dashCooldown));

        this.maxMidairJumps = maxMidairJumps;
        this.dashCooldown = dashCooldown;
        this.dashSpeed = dashSpeed;
        
        _moveAction = PlayerManager.PlayerInput.actions.FindAction("Move");
        _dashAction = PlayerManager.PlayerInput.actions.FindAction("Dash");
        _jumpAction = PlayerManager.PlayerInput.actions.FindAction("Jump");
    }

    private void Update()
    {
        if(_dashAction.triggered) Dash();
        if(_jumpAction.triggered) DoubleJump();
        if (PlayerManager.Movement.grounded) _jumps = maxMidairJumps;
    }

    private void Dash()
    {
        if (!TimerManager.CheckTimer("DashCooldown")) return;
        
        float targetAngle = PlayerManager.GetAngleTowardsVectorFromCamera(_moveAction.ReadValue<Vector2>());
        
        TimerManager.ResetTimer("DashCooldown");

        Vector3 move = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * dashSpeed;

        PlayerManager.Movement.SetMovementVector(new MovementVector(move, 1, false));
    }

    private void DoubleJump()
    {
        if (PlayerManager.Movement.grounded || _jumps < 1) return;
        _jumps--;
        PlayerManager.Movement.Jump();
    }
}
