using System;
using System.Collections.Generic;
using UnityEngine;

public class RapidAgeMono : MonoBehaviour
{
    [SerializeField] LayerMask blockingLayers;
    List<GameObject> _damageableEnemies = new List<GameObject>();

    float _timer;
    float _maxTime = 0.5f;
    
    void OnTriggerEnter(Collider other)
    {
        if (!Physics.Linecast(transform.position, other.transform.position, blockingLayers))
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
            
            _damageableEnemies.Add(other.gameObject);
            Debug.Log("Added!");
        }
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer < _maxTime) return;
        
        foreach (GameObject enemy in _damageableEnemies)
        {
            if (!enemy) continue;
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            
            damageable?.Damage(5);
        }

        _timer = 0;
    }
}
