using System;
using UnityEngine;

public class Wolf : BasicEnemy
{
    [SerializeField] float biteInterval;
    [SerializeField] float biteDamage;
    
    float _biteTimer;

    void Update()
    {
        if (_biteTimer >= 0) _biteTimer -= Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger");
        if (_biteTimer <= 0)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            
            if(damageable == null) return;
            
            damageable.Damage(biteDamage);
            _biteTimer = biteInterval;
        }
    }
}
        

