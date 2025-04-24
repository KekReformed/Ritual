using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosionRadius : MonoBehaviour
{
    [SerializeField] LayerMask blockingLayers;
    Dictionary<string, GameObject> _damageableEnemies = new Dictionary<string, GameObject>();
    int _count;
    
    void OnTriggerStay(Collider other)
    {
        if (!Physics.Linecast(transform.position, other.transform.position, blockingLayers))
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
            if(_damageableEnemies.ContainsKey(other.gameObject.name)) return;
            
            _damageableEnemies.Add(_count.ToString(), other.gameObject);
            other.gameObject.name = _count.ToString();
            
            _count++;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (_damageableEnemies.ContainsKey(other.gameObject.name))
            {
                _damageableEnemies.Remove(other.gameObject.name);
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
        
        if(_damageableEnemies.ContainsKey(other.gameObject.name)) _damageableEnemies.Remove(other.gameObject.name);
    }

    public void DamageEnemies(float damage)
    {
        foreach (GameObject damageableEnemy in _damageableEnemies.Values)
        {
            IDamageable damageable = damageableEnemy.GetComponent<IDamageable>();

            damageable?.Damage(damage);
        }
    }
}
