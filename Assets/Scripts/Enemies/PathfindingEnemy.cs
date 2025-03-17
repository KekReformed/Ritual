using UnityEngine;
using UnityEngine.AI;

public class PathfindingEnemy : MonoBehaviour
{
   GameObject target;
   public NavMeshAgent agent;

   float moveSpeed;
   public float aggroRange;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

 
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance <= aggroRange)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

    }

}