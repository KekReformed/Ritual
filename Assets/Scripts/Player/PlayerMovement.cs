using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput _playerInput;
    InputAction _moveAction;
    InputAction _jumpAction;
    CharacterController _characterController;
    Camera _cam;

    Vector3 _velocity;
    
    [SerializeField] float speed;
    [SerializeField] float gravityScale = 1;
    [SerializeField] float jumpForce;
    
    public bool grounded;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("Move");
        _jumpAction = _playerInput.actions.FindAction("Jump");
        _characterController = GetComponent<CharacterController>();
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!grounded) ApplyGravity();
        else _velocity.y = 0f;
        if (grounded && _jumpAction.triggered) Jump();
        
        
        Move();
        _characterController.Move(_velocity * Time.deltaTime);
    }

    void Move()
    {
        //Get a reference movement vector so we don't have to call the function multiple times, if we aren't currently inputing any movement ignore this
        Vector3 moveInput = _moveAction.ReadValue<Vector2>();
        
        if (moveInput.magnitude < 0.1f)
        {
            _velocity.x = 0f;
            _velocity.z = 0f;
            return;
        }
        
        //Rotate towards the direction that the camera is facing when we move 
        float targetAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg + _cam.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        Vector3 move = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * speed;
        _velocity.x = move.x;
        _velocity.z = move.z;
    }
    
    void ApplyGravity()
    {
        _velocity.y += -9.81f * gravityScale * Time.deltaTime;
    }

    void Jump()
    {
        grounded = false;
        _velocity.y = jumpForce;
    }
}
