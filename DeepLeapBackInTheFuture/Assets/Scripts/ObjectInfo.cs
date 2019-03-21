using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo
{
    public enum Type
    {
        DEFAULT,
        ENEMY,
        PLAYER,
        BULLET
    }

    public Vector3 position;
    public Quaternion rotation;
    public float createdAt;
    public Type type;

    public ObjectInfo(Vector3 position, Quaternion rotation, Type type = Type.DEFAULT)
    {
        this.position = position;
        this.rotation = rotation;
        this.type = type;
        createdAt = Time.time;
    }

    public void update(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
        createdAt = Time.time;
    }
}
