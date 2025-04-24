using System;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    PlayerMovement _playerMovement;

    [SerializeField] [Range(20, 180)] float angleTolerance;
    [SerializeField] Terrain terrain;

    void Awake()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
        if(terrain == null) Debug.LogError("No terrain assigned to player!");
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerManager.Movement.grounded = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (GetNormalAngle(terrain) < angleTolerance) PlayerManager.Movement.grounded = true;
        else PlayerManager.Movement.grounded = false;
    }

    void OnTriggerExit(Collider other)
    {
        PlayerManager.Movement.grounded = false;
    }

    float GetNormalAngle(Terrain terrain)
    {
        Vector3 terrainPos = terrain.transform.position;
        Vector3 position = transform.position;
        
        TerrainData terrainData = terrain.terrainData;
        
        // Convert world position to normalized terrain coordinates
        float normX = (position.x - terrainPos.x) / terrainData.size.x;
        float normZ = (position.z - terrainPos.z) / terrainData.size.z;

        Vector3 normal = terrainData.GetInterpolatedNormal(normX, normZ);

        return Vector3.Angle(normal,Vector3.up);
    }
}
