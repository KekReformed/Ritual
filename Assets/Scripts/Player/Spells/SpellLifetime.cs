using UnityEngine;

public class SpellLifetime : MonoBehaviour
{

    [HideInInspector] public float lifeTime;
    [SerializeField] [Tooltip("Lifetime in seconds before deleting this object")] float maxLifetime;
    
    void Update()
    {
        lifeTime += Time.deltaTime;
        if(lifeTime > maxLifetime) Destroy(gameObject);
    }
}
