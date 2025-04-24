using System;
using UnityEngine;

public class CrowStormDamage : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player" || LayerMask.NameToLayer("Spell") == other.gameObject.layer) return;
        
        IDamageable damageable = other.GetComponent<IDamageable>();
        
        if (damageable == null)
        {
            Destroy(gameObject);
            return;
        }
        
        damageable.Damage(7);
        Destroy(gameObject);
    }
}
