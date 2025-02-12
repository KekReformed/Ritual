using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "BasicProjectileSpell", menuName = "Spells/Basic Projectile")]
public class BasicProjectileSpell : Spell
{
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] float projectileSpeed;
    
    public override void Use()
    {
        base.Use();
        RotateTowardsCameraDir();
            
        GameObject fireballObject = Instantiate(fireballPrefab,PlayerManager.Instance.transform.position + PlayerManager.Instance.transform.rotation * Vector3.forward.normalized, Quaternion.identity);
        Rigidbody rb = fireballObject.GetComponent<Rigidbody>();
        rb.linearVelocity = PlayerManager.Instance.transform.rotation * Vector3.forward.normalized * projectileSpeed;
    }
}
