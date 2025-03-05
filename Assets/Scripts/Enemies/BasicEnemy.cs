using UnityEngine;
using UnityEngine.Serialization;

public class BasicEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float health;
    
    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
        Debug.Log("Ow!");
    }
}
