using UnityEngine;

[CreateAssetMenu(fileName = "BasicProjectileSpell", menuName = "Spells/Active/Basic Projectile")]
public class BasicProjectileSpell : ActiveSpell
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;
    
    public override void Use()
    {
        base.Use();
        float targetAngle = PlayerManager.GetAngleTowardsVectorFromCamera(Vector3.forward);
        
        PlayerManager.Instance.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            
        GameObject fireballObject = Instantiate(projectilePrefab,PlayerManager.Instance.transform.position + PlayerManager.Instance.transform.rotation * Vector3.forward.normalized, Quaternion.identity);
        Rigidbody rb = fireballObject.GetComponent<Rigidbody>();
        rb.linearVelocity = PlayerManager.Instance.transform.rotation * Vector3.forward.normalized * projectileSpeed;
    }
}
