using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    PlayerMovement _playerMovement;

    void Awake()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerManager.Movement.grounded = true;
    }
    
    void OnTriggerExit(Collider other)
    {
        PlayerManager.Movement.grounded = false;
    }
}
