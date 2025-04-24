using System;
using UnityEngine;

public class HolyBall : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float damage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = (PlayerManager.Instance.transform.position - transform.position).normalized * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Enemy" || LayerMask.NameToLayer("Spell") == other.gameObject.layer || LayerMask.NameToLayer("Trigger") == other.gameObject.layer) return;
        
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
