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
        PlayerManager.Instance.movement.grounded = true;
    }
    
    void OnTriggerExit(Collider other)
    {
        PlayerManager.Instance.movement.grounded = false;
    }
}
