using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class Wolf : BasicEnemy
{
    [SerializeField] float biteInterval;
    [SerializeField] float biteDamage;
    
    public Vector3 direction;
    public float speed = 5.0f;
     private Rigidbody rb;
     public float changeDirectionInterval = 2.0f;
    private float timer;
    float randomAngle;

    
    float _biteTimer;
 void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        ChangeDirection();
    }

    void Update()
    {
        if (_biteTimer >= 0) _biteTimer -= Time.deltaTime;
    }
        void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction.normalized * speed * Time.fixedDeltaTime);

         timer += Time.fixedDeltaTime;
        if (timer >= changeDirectionInterval)
        {
            ChangeDirection();
            timer = 0f;
        }
        
    }


    void OnTriggerStay(Collider other)
    {
        if (_biteTimer <= 0)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            
            if(damageable == null) return;
            
            damageable.Damage(biteDamage);
            _biteTimer = biteInterval;
        }
    }
 void ChangeDirection()
    {
         randomAngle = Random.Range(0, 360);
        direction = new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle)).normalized;
    }
void OnDrawGizmos()
{
    Gizmos.color = Color.red;
    Gizmos.DrawLine(transform.position, transform.position + direction.normalized * 2.0f);
}


}
        

