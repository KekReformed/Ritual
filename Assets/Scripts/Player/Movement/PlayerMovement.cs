using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _playerInput;
    InputAction _moveAction;
    InputAction _jumpAction;
    InputAction _dashAction;
    
    CharacterController _characterController;
    Camera _cam;
    const string DashName = "Dash";

    readonly MovementVector _defaultVector = new MovementVector(Vector3.zero,-10);
    
    Vector3 _lastMovementInput;
    Vector3 _velocity;
    
    [Header("Basic Stats")]
    [SerializeField] float speed;
    [SerializeField] float airDeceleration;
    [SerializeField] float deceleration;
    [SerializeField] float gravityScale = 1;
    [SerializeField] float jumpForce;
    
    [Header("Dashing")]
    [Tooltip("How long in seconds the dash lasts")]
    [SerializeField] float dashTime;
    [Tooltip("The multiplier applied to normal speed whilst dashing")]
    [SerializeField] float dashSpeed;
    [Tooltip("The vertical velocity to be applied during the dash")] 
    [SerializeField] float dashHeight;
    [Tooltip("The amount of time in seconds we have to wait to dash again after the last dash has ended")]
    [SerializeField] float dashCooldown;
    
    [HideInInspector] public bool grounded;
    [HideInInspector] public Vector3 lastMovementVector; 
    [HideInInspector] public MovementVector currentMovementVector;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerInput = PlayerManager.PlayerInput;
        _moveAction = _playerInput.actions.FindAction("Move");
        _jumpAction = _playerInput.actions.FindAction("Jump");
        _dashAction = _playerInput.actions.FindAction("Dash");
        
        _characterController = GetComponent<CharacterController>();
        
        _cam = Camera.main;
        currentMovementVector = _defaultVector;
        TimerManager.AddTimer(new Timer(DashName,dashTime));
        TimerManager.AddTimer(new Timer("DashCooldown",dashCooldown + dashTime));
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 decelVector = Vector3.zero;
        
        
        //Decelerate us if we were moving last frame, this only gets applied if our new vector after deceleration would be faster than _currentMovementVector
        if (grounded)
        {
            if (lastMovementVector.x != 0) decelVector.x = lastMovementVector.x - deceleration * lastMovementVector.normalized.x * Time.deltaTime; 
            if (lastMovementVector.z != 0) decelVector.z = lastMovementVector.z - deceleration * lastMovementVector.normalized.z * Time.deltaTime; 
        }
        else
        {
            if (lastMovementVector.x != 0) decelVector.x = lastMovementVector.x - airDeceleration * lastMovementVector.normalized.x * Time.deltaTime; 
            if (lastMovementVector.z != 0) decelVector.z = lastMovementVector.z - airDeceleration * lastMovementVector.normalized.z * Time.deltaTime; 
        }
        
        //If deceleration would result in us actually increasing our speed (e.g. we decelrate 1.0 by 2.0 we would be going at -1.0 speed) then set our decelVector to 0;
        if (Mathf.Sign(decelVector.x) != Mathf.Sign(lastMovementVector.x)) decelVector.x = 0;
        if (Mathf.Sign(decelVector.z) != Mathf.Sign(lastMovementVector.z)) decelVector.z = 0;
        
        
        
        if (!grounded) ApplyGravity();
        else _velocity.y = 0f;
        
        if (grounded && _jumpAction.triggered) Jump();
        
        Dash();
        
        MovementVector tempMoveVector = Move();

        if (CheckPriority(tempMoveVector)) currentMovementVector = tempMoveVector;

        Vector3 tempVector = currentMovementVector.vector;
        
        if (currentMovementVector.applyGravity)
        {
            tempVector.y += _velocity.y;
        }
        else
        {
            _velocity.y = 0f;
        }

        //If the horizonatal speed we would get from our current vector is less than the last vector deccelerated then use the deccelerated vector rather than the new vector
        if (Mathf.Abs(currentMovementVector.vector.x) < Mathf.Abs(decelVector.x)) tempVector.x = decelVector.x;
        if (Mathf.Abs(currentMovementVector.vector.z) < Mathf.Abs(decelVector.z)) tempVector.z = decelVector.z;
        
        _characterController.Move(tempVector * Time.deltaTime);

        lastMovementVector = tempVector;
        currentMovementVector = _defaultVector;
    }

    
    
    MovementVector Move()
    {
        //Get a reference movement vector so we don't have to call the function multiple times, if we aren't currently inputting any movement ignore this
        Vector3 moveInput = _moveAction.ReadValue<Vector2>();
        if(moveInput.magnitude < 0.1f && TimerManager.CheckTimer(DashName)) return new MovementVector(Vector3.zero, -10);
        if(moveInput.magnitude < 0.1f && !TimerManager.CheckTimer(DashName)) moveInput = _lastMovementInput;

        bool applyGravity = true;
        int priority = 0;
        
        //Rotate towards the direction were going to move, based on the way the camera is facing
        float targetAngle = GetAngleTowardsVector(moveInput);
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        
        //Move in the direction the camera is facing
        Vector3 move = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * speed;

        move.y = 0;
        
        if (!TimerManager.CheckTimer(DashName))
        {
            move *= dashSpeed;
            applyGravity = false;
            if(!grounded) move.y += dashHeight;
            priority = 2;
        }

        _lastMovementInput = moveInput;
        return new MovementVector(new Vector3(move.x, move.y, move.z), priority, applyGravity);
    }
    
    void ApplyGravity()
    {
        _velocity.y += -9.81f * gravityScale * Time.deltaTime;
    }

    bool CheckPriority(MovementVector movementVector)
    {
        return movementVector.priority > currentMovementVector.priority;
    }

    void Dash()
    {
        if (TimerManager.CheckTimer(DashName) && currentMovementVector.priority < 3)
        {
            currentMovementVector.applyGravity = true;
        }
        if (!_dashAction.triggered || !TimerManager.CheckTimer("DashCooldown")) return;
        
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
