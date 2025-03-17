using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wolf : BasicEnemy
{
    [SerializeField] float biteInterval;
    [SerializeField] float biteDamage;

    public Vector3 direction;

    float _biteTimer;

    void Update()
    {
        if (_biteTimer >= 0) _biteTimer -= Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        if (_biteTimer <= 0)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable == null) return;

            damageable.Damage(biteDamage);
            _biteTimer = biteInterval;
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction.normalized * 2.0f);
    }
}
