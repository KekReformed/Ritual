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

    readonly MovementVector _defaultVector = new MovementVector(Vector3.zero,-10);
    
    Vector3 _lastMovementInput;
    Vector3 _velocity;
    
    Vector3 _lastMovementVector; 
    MovementVector _currentMovementVector;
    
    [Header("Basic Stats")]
    [SerializeField] float speed;
    [SerializeField] float airDeceleration;
    [SerializeField] float deceleration;
    [SerializeField] float gravityScale = 1;
    [SerializeField] float jumpForce;
    
    [HideInInspector] public bool grounded;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerInput = PlayerManager.PlayerInput;
        _moveAction = _playerInput.actions.FindAction("Move");
        _jumpAction = _playerInput.actions.FindAction("Jump");
        _dashAction = _playerInput.actions.FindAction("Dash");
        
        _characterController = GetComponent<CharacterController>();
        
        _cam = Camera.main;
        _currentMovementVector = _defaultVector;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 decelVector = Vector3.zero;
        
        
        //Decelerate us if we were moving last frame, this only gets applied if our new vector after deceleration would be faster than _currentMovementVector
        if (grounded)
        {
            if (_lastMovementVector.x != 0) decelVector.x = _lastMovementVector.x - deceleration * _lastMovementVector.normalized.x * Time.deltaTime; 
            if (_lastMovementVector.z != 0) decelVector.z = _lastMovementVector.z - deceleration * _lastMovementVector.normalized.z * Time.deltaTime; 
        }
        else
        {
            if (_lastMovementVector.x != 0) decelVector.x = _lastMovementVector.x - airDeceleration * _lastMovementVector.normalized.x * Time.deltaTime; 
            if (_lastMovementVector.z != 0) decelVector.z = _lastMovementVector.z - airDeceleration * _lastMovementVector.normalized.z * Time.deltaTime; 
        }
        
        //If deceleration would result in us actually increasing our speed (e.g. we decelrate 1.0 by 2.0 we would be going at -1.0 speed) then set our decelVector to 0;
        if (Mathf.Sign(decelVector.x) != Mathf.Sign(_lastMovementVector.x)) decelVector.x = 0;
        if (Mathf.Sign(decelVector.z) != Mathf.Sign(_lastMovementVector.z)) decelVector.z = 0;
        
        
        
        if (!grounded) ApplyGravity();
        else _velocity.y = 0f;
        
        if (grounded && _jumpAction.triggered) Jump();
        
        MovementVector tempMoveVector = Move();

        if (CheckMovementVectorPriority(tempMoveVector)) _currentMovementVector = tempMoveVector;

        Vector3 tempVector = _currentMovementVector.vector;
        
        if (_currentMovementVector.applyGravity)
        {
            tempVector.y += _velocity.y;
        }
        else
        {
            _velocity.y = 0f;
        }

        //If the horizonatal speed we would get from our current vector is less than the last vector deccelerated then use the deccelerated vector rather than the new vector
        if (Mathf.Abs(_currentMovementVector.vector.x) < Mathf.Abs(decelVector.x)) tempVector.x = decelVector.x;
        if (Mathf.Abs(_currentMovementVector.vector.z) < Mathf.Abs(decelVector.z)) tempVector.z = decelVector.z;
        
        _characterController.Move(tempVector * Time.deltaTime);

        _lastMovementVector = tempVector;
        _currentMovementVector = _defaultVector;
    }

    
    
    MovementVector Move()
    {
        //Get a reference movement vector so we don't have to call the function multiple times, if we aren't currently inputting any movement ignore this
        Vector3 moveInput = _moveAction.ReadValue<Vector2>();
        if(moveInput.magnitude < 0.1f) return new MovementVector(Vector3.zero, -10);
        
        //Rotate towards the direction were going to move, based on the way the camera is facing
        float targetAngle = PlayerManager.GetAngleTowardsVectorFromCamera(moveInput);
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        
        //Move based on the direction the camera is facing
        Vector3 move = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * speed;

        move.y = 0;

        _lastMovementInput = moveInput;
        return new MovementVector(new Vector3(move.x, move.y, move.z), 0, true);
    }
    
    void ApplyGravity()
    {
        _velocity.y += -9.81f * gravityScale * Time.deltaTime;
    }

    bool CheckMovementVectorPriority(MovementVector movementVector)
    {
        return movementVector.priority > _currentMovementVector.priority;
    }

    public void Jump()
    {
        grounded = false;
        _velocity.y = jumpForce;
    }

    /// <summary>
    /// Sets the movement vector of the player, returns true if succesful and false if not
    /// </summary>
    public bool SetMovementVector(MovementVector movementVector)
    {
        if (movementVector.priority <= _currentMovementVector.priority) return false;
        _currentMovementVector = movementVector;
        return true;
    }
}
