using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BasicEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float health;
    [SerializeField] protected PathfindingEnemy pathfinding;

    void Start()
    {
        pathfinding = GetComponentInParent<PathfindingEnemy>();
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
        pathfinding.agent.SetDestination(PlayerManager.Instance.transform.position);
    }
}
