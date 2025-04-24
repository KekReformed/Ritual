using System.Collections.Generic;
using UnityEngine;

public class CrowStormMono : MonoBehaviour
{
    [SerializeField] LayerMask blockingLayers;
    [SerializeField] float speed;

    float _damage;
    GameObject _target;
    
    void OnTriggerStay(Collider other)
    {
        if (!Physics.Linecast(transform.position, other.transform.position, blockingLayers))
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

            if (!_target || Vector3.Distance(transform.position, _target.transform.position) > Vector3.Distance(transform.position, other.transform.position))
            {
                _target = other.gameObject;
            }
        }
    }

    void Update()
    {
        Vector3 direction;
        if (_target == null)
        {
            direction = transform.rotation * Vector3.forward;
            
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position += direction * speed * Time.deltaTime;
            
            return;
        }

        direction = (_target.transform.position - transform.position).normalized;
        
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position += direction * speed * Time.deltaTime;
    }
}
