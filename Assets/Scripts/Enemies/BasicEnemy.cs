using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class BasicEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] protected float health;
    [SerializeField] protected PathfindingEnemy pathfinding;
    [SerializeField] protected string enemyID;
    [SerializeField] GameObject damageText;
    [SerializeField] float damageTextOffset;

    void Start()
    {
        pathfinding = GetComponentInParent<PathfindingEnemy>();
        if(enemyID == "") Debug.LogError("Enemy missing ID!");
    }

    public void Damage(float damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Destroy(gameObject);
            PlayerManager.PlayerStats.IncrementStat(enemyID,"Kills");
            QuestManager.CheckKillObjectives();
        }
        
        pathfinding.agent.SetDestination(PlayerManager.Instance.transform.position);

        GameObject damageTextObject = Instantiate(damageText, transform.position + new Vector3(0,damageTextOffset,0), Quaternion.identity);
        damageTextObject.GetComponent<DamageText>().Setup(damage);
    }
}
