using System;
using UnityEngine;

public class MeteorMono : MonoBehaviour
{
    public Vector3 target;
    public float damage;
    ExplosionRadius _explosionRadius;
    
    void Start()
    {
        _explosionRadius = GetComponentInChildren<ExplosionRadius>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player" || LayerMask.NameToLayer("Spell") == other.gameObject.layer) return;
        Debug.Log(other.gameObject.name);
        IDamageable damageable = other.GetComponent<IDamageable>();
        
        if (damageable == null)
        {
            _explosionRadius.DamageEnemies(damage);
            Destroy(gameObject);
            return;
        }
        
        damageable.Damage(damage);
        _explosionRadius.DamageEnemies(damage);
        Destroy(gameObject);
    }
}
