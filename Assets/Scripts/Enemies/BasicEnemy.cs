using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class BasicEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float health;
    [SerializeField] protected PathfindingEnemy pathfinding;
    [SerializeField] GameObject damageText;
    [SerializeField] float damageTextOffset;

    void Start()
    {
        pathfinding = GetComponentInParent<PathfindingEnemy>();
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
        pathfinding.agent.SetDestination(PlayerManager.Instance.transform.position);

        GameObject damageTextObject = Instantiate(damageText, transform.position + new Vector3(0,damageTextOffset,0), Quaternion.identity);
        damageTextObject.GetComponent<DamageText>().Setup(damage);
    }
}
