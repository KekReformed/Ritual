using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "Spells/Active/Fireball")]
public class BasicProjectileSpell : ActiveSpell
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] float damage;
    
    public override bool Use()
    {
        if (PlayerManager.Instance.Upgrades.Contains("FireballUpgrade")) cost -= 5;
        if (!base.Use())
        {
            if (PlayerManager.Instance.Upgrades.Contains("FireballUpgrade")) cost += 5;
            return false;
        }
        float targetAngle = PlayerManager.GetAngleTowardsVectorFromCamera(Vector3.forward);
        
        PlayerManager.Instance.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            
        GameObject fireballObject = Instantiate(projectilePrefab,PlayerManager.Instance.transform.position + PlayerManager.Instance.transform.rotation * Vector3.forward.normalized, Quaternion.identity);
        Rigidbody rb = fireballObject.GetComponent<Rigidbody>();
        
        fireballObject.GetComponent<FireballMono>().Setup(damage);
        
        rb.linearVelocity = PlayerManager.Instance.transform.rotation * Vector3.forward.normalized * projectileSpeed;

        if (PlayerManager.Instance.Upgrades.Contains("FireballUpgrade")) cost += 5;
        return true;
    }
}
