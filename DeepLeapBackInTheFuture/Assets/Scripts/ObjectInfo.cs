using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo
{
    public Vector3 position;
    public Quaternion rotation;
    public float lastUpdated;

    public ObjectInfo(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
        lastUpdated = Time.time;
    }
}
