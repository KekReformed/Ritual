using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class MovementVector
{    
    public Vector3 vector;
    public bool applyGravity;
    public int priority;

    /// <param name="vector">Vector to apply, ALWAYS LEAVE Y AS ZERO UNLESS YOU HAVE A VERY GOOD REASON!</param>
    /// <param name="priority">Priority of the vector, e.g. if we have two vectors applied then the vector with the higher priority gets applied</param>
    /// <param name="gravity">Whether to apply gravity whilst this vector is applied</param>
    public MovementVector(Vector3 vector, int priority, bool gravity = true)
    {
        this.vector = vector;
        this.priority = priority;
        applyGravity = gravity;
    }
}
