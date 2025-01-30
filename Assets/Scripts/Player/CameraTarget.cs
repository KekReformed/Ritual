using UnityEngine;
using UnityEngine.Serialization;

public class CameraTarget : MonoBehaviour
{
    Transform player;
    public float followSpeed = 10;
    public float verticalMultiplier = 2;
    Vector3 velocity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = PlayerManager.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = (player.position - transform.position) * Time.deltaTime * followSpeed * Vector3.Distance(player.position,transform.position);
        velocity.y *= verticalMultiplier;
        transform.position += velocity;
    }
}
