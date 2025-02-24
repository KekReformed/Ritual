using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PathfindingEnemy : MonoBehaviour
{
   private GameObject target;
   private NavMeshAgent agent;

   private float moveSpeed;
    public float aggroRange;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

 
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance<= aggroRange)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

    }

}
