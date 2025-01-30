using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput _playerInput;
    InputAction _moveAction;
    InputAction _jumpAction;
    InputAction _dashAction;
    
    CharacterController _characterController;
    Camera _cam;
    const string DashName = "Dash";

    readonly MovementVector _defaultVector = new MovementVector(Vector3.zero,-10);
    MovementVector _currentMovementVector;
    
    Vector3 _velocity;
    
    [Header("Basic Stats")]
    [SerializeField] float speed;
    [SerializeField] float gravityScale = 1;
    [SerializeField] float jumpForce;
    
    [Header("Dashing")]
    [Tooltip("How long in seconds the dash lasts")]
    [SerializeField] float dashTime;
    [Tooltip("The multiplier applied to normal speed whilst dashing")]
    [SerializeField] float dashSpeed;
    [Tooltip("The amount of time in seconds we have to wait to dash again after the last dash has ended")]
    [SerializeField] float dashCooldown;
    
    [HideInInspector]
    public bool grounded;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("Move");
        _jumpAction = _playerInput.actions.FindAction("Jump");
        _dashAction = _playerInput.actions.FindAction("Dash");
        
        _characterController = GetComponent<CharacterController>();
        
        _cam = Camera.main;
        _currentMovementVector = _defaultVector;
        TimerManager.AddTimer(new Timer(DashName,dashTime));
        TimerManager.AddTimer(new Timer("DashCooldown",dashCooldown + dashTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (!grounded) ApplyGravity();
        else _velocity.y = 0f;
        
        if (grounded && _jumpAction.triggered) Jump();
        if (_dashAction.triggered) Dash();
        
        Move();

        Vector3 tempVector = _currentMovementVector.vector;

        if (_currentMovementVector.applyGravity)
        {
            tempVector += _velocity;
        }
        else
        {
            _velocity.y = 0f;
        }
        
        _characterController.Move((tempVector) * Time.deltaTime);
        
        //This must be done at the end of movement
        _currentMovementVector = _defaultVector;
    }

    void Move()
    {
        //Get a reference movement vector so we don't have to call the function multiple times, if we aren't currently inputting any movement ignore this
        Vector3 moveInput = _moveAction.ReadValue<Vector2>();
        if(moveInput.magnitude < 0.1f || _currentMovementVector.priority > 0) return;

        bool applyGravity = true;
        //Rotate towards the direction were going to move, based on the way the camera is facing
        float targetAngle = GetAngleTowardsVector(moveInput);
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        
        //Move in the direction the camera is facing
        Vector3 move = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * speed;

        if (TimerManager.Timers[DashName].CurrentTime > 0f)
        {
            move *= dashSpeed;
            applyGravity = false;
        }
        
        _currentMovementVector = new MovementVector(new Vector3(move.x, 0f, move.z), 0,applyGravity);
    }
    
    void ApplyGravity()
    {
        _velocity.y += -9.81f * gravityScale * Time.deltaTime;
    }

    void Dash()
    {
        if(TimerManager.Timers["DashCooldown"].CurrentTime > 0f) return;
        
        TimerManager.ResetTimer(DashName);
        TimerManager.ResetTimer("DashCooldown");
    }

    /// <summary>
    /// Gets the angle between the characters forward direction, and the way the camera is currently facing useful for correcting movement towards camera
    /// </summary>
    float GetAngleTowardsVector(Vector2 targetVector)
    {
        float targetAngle = Mathf.Atan2(targetVector.x, targetVector.y) * Mathf.Rad2Deg + _cam.transform.eulerAngles.y;

        return targetAngle;
    }

    void Jump()
    {
        grounded = false;
        _velocity.y = jumpForce;
    }
}
