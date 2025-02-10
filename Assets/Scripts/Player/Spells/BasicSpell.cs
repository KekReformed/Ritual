using UnityEngine;
using UnityEngine.InputSystem;

public class BasicSpell : MonoBehaviour
{
    PlayerInput _playerInput;
    InputAction _attack;

    [SerializeField] GameObject fireball;
    [SerializeField] private Transform aimpoint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       _playerInput = PlayerManager.PlayerInput;
       _attack = _playerInput.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject fireballObject;

        if (_attack.triggered && PlayerManager.Instance.Mana >= 20)
        {
            //Rotate player towards spell direction
            float targetAngle = Mathf.Atan2(Vector3.forward.x, Vector3.forward.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            PlayerManager.Instance.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            
            fireballObject = Instantiate(fireball,PlayerManager.Movement.transform.position,Quaternion.identity);
            fireballObject.GetComponent<Rigidbody>().linearVelocity = (aimpoint.position - transform.position).normalized * 2;
            
            PlayerManager.SetMana(PlayerManager.Instance.Mana - 20);
        }
    }
}
