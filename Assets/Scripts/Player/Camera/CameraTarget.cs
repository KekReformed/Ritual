using UnityEngine;
using UnityEngine.Serialization;

public class CameraTarget : MonoBehaviour
{
    Transform _player;
    public float followSpeed = 10;
    public float verticalMultiplier = 2;
    public Vector3 offset;
    Vector3 _targetPosition;
    Vector3 _velocity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = PlayerManager.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _targetPosition = _player.position + offset;
        
        _velocity = (_targetPosition - transform.position) * Time.deltaTime * followSpeed * Mathf.Min(Vector3.Distance(_targetPosition, transform.position), 10);
        _velocity.y *= verticalMultiplier;
        transform.position += _velocity;
    }
}
