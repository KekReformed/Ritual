using System;
using UnityEngine;

public class FireballMono : MonoBehaviour
{
    public float damage;

    public void Setup(float damage)
    {
        this.damage = damage;
    }
    
    void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        
        if (damageable == null)
        {
            Destroy(gameObject);
            return;
        }
        
        damageable.Damage(damage);
        Destroy(gameObject);
    }
}
